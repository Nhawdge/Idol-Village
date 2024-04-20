using Arch.Core;
using Arch.Core.Extensions;
using Raylib_cs;
using System.Net;
using System.Numerics;
using VillageIdle.Scenes.Components;
using VillageIdle.Scenes.World1.Data;
using VillageIdle.Scenes.World1.Helpers;
using VillageIdle.Utilities;

namespace VillageIdle.Scenes.World1.Systems
{
    internal class UISystem : GameSystem
    {
        private static readonly int SideBarWidth = 350;
        private static readonly int SideBarPadding = 10;
        private static readonly int SideBarInnerWidth = 330;
        internal override void Update(World world) { }
        internal override void UpdateNoCamera(World world)
        {
            var producerQuery = new QueryDescription().WithAll<ProductionUnit>();

            var producers = new Dictionary<ProducerTypes, int>();

            world.Query(in producerQuery, (entity) =>
            {
                var productionUnit = entity.Get<ProductionUnit>();
                if (!producers.TryAdd(productionUnit.Producer, 1))
                {
                    producers[productionUnit.Producer]++;
                }
            });

            var unitQuery = new QueryDescription().WithAll<Unit>();

            var availableUnits = new List<Entity>();
            var totalUnits = 0;
            var allUnits = new List<Entity>();
            world.Query(in unitQuery, (entity) =>
            {
                var unit = entity.Get<Unit>();
                if (!unit.AssignedTo.IsAlive())
                {
                    availableUnits.Add(entity);
                }
                allUnits.Add(entity);
                totalUnits++;
            });

            var query = new QueryDescription().WithAny<Interactable>();
            var mousePos = Raylib.GetMousePosition();
            world.Query(in query, (entity) =>
            {
                if (entity.Id == Singleton.Instance.SelectedUnit)
                {
                    return;
                }
                var interactable = entity.Get<Interactable>();
                var patch = new NPatchInfo
                {
                    Left = 10,
                    Top = 10,
                    Right = 10,
                    Bottom = 10,
                    Layout = NPatchLayout.NinePatch
                };

                var backgroundTexture = TextureManager.Instance.GetTexture(TextureKey.BrownBox);

                patch.Source = new Rectangle(0, 0, backgroundTexture.Width, backgroundTexture.Height);
                Raylib.DrawTextureNPatch(backgroundTexture, patch, new Rectangle(0, 0, SideBarWidth, Raylib.GetScreenHeight()), Vector2.Zero, 0f, Color.White);

                var text = "Pavekstan";
                var rect = new Rectangle(10, 10, SideBarInnerWidth, 40);
                var size = Raylib.MeasureTextEx(VillageIdleEngine.Instance.Font, text, 24, 0);
                var position = new Vector2((int)(rect.X + (rect.Width / 2) - (size.X / 2)), (int)rect.Y + rect.Height / 2 - (size.Y / 2));
                Raylib.DrawTextureNPatch(TextureManager.Instance.GetTexture(TextureKey.BlueBox), patch, rect, Vector2.Zero, 0f, Color.White);
                Raylib.DrawTextEx(VillageIdleEngine.Instance.Font, text, position, 24, 0f, Color.Black);

                text = string.Join("\n", VillageData.Instance.Resources.Where(x => x.Value > 0).Select(x => $"{x.Key}: {x.Value}"));
                UiHelpers.DrawTextWithBackground(TextureKey.BlueBox, text, new Vector2(10, 60));

                var yIndex = 0;
                var yStart = 300;
                var yIncrement = 35;

                text = "Research";
                size = Raylib.MeasureTextEx(VillageIdleEngine.Instance.Font, text, 24, 0);
                position = new Vector2(10 + (SideBarInnerWidth / 2) - (size.X / 2), yStart + yIndex * yIncrement);
                Raylib.DrawTextEx(VillageIdleEngine.Instance.Font, text, position, 24, 0f, Color.Black);
                yStart += yIncrement;

                foreach (var research in TechTree.Instance.GetAvailableTechnologies())
                {
                    var canAfford = true;
                    if (research.Costs != null)
                    {
                        foreach (var cost in research.Costs)
                        {
                            if (VillageData.Instance.Resources[cost.Key] <= cost.Value)
                            {
                                canAfford = false;
                                break;
                            }
                        }
                    }

                    if (UiHelpers.DrawButtonWithBackground(TextureKey.BlueBox, $"{research.Name}", new Vector2(10, yStart + yIndex * yIncrement), !canAfford))
                    {
                        if (!canAfford)
                        {
                            continue;
                        }

                        foreach (var cost in research.Costs)
                        {
                            VillageData.Instance.Resources[cost.Key] -= cost.Value;
                        }

                        //pass
                        research.Researched = true;
                        foreach (var producerKey in research.ProductionToAdd)
                        {
                            ProducerStore.Instance.Producers[producerKey].IsAvailable = true;
                        }
                    }
                    yIndex++;
                }

                text = $"Jobs - Idle: {availableUnits.Count},  Total: {totalUnits}";
                size = Raylib.MeasureTextEx(VillageIdleEngine.Instance.Font, text, 24, 0);
                position = new Vector2(10 + (SideBarInnerWidth / 2) - (size.X / 2), yStart + yIndex * yIncrement);
                Raylib.DrawTextEx(VillageIdleEngine.Instance.Font, text, position, 24, 0f, Color.Black);
                yStart += yIncrement;

                foreach (var production in ProducerStore.Instance.GetAvailableProducers())
                {
                    UiHelpers.DrawTextWithBackground(TextureKey.BlueBox, production.Name, new Vector2(10, yStart + yIndex * yIncrement));

                    var unitsInRole = producers.FirstOrDefault(x => x.Key == production.Key).Value.ToString() ?? "0";
                    if (UiHelpers.DrawImageAsButton(TextureKey.ArrowSilverDown, new Vector2(200, yStart + yIndex * yIncrement + 5), unitsInRole == "0"))
                    {
                        var deleted = false;
                        world.Query(in producerQuery, (entity) =>
                        {
                            if (deleted) { return; }
                            var productionUnit = entity.Get<ProductionUnit>();
                            if (productionUnit.Producer == production.Key)
                            {
                                world.Destroy(entity);
                                deleted = true;
                            }

                            var assignedUnit = allUnits.FirstOrDefault(x => x.Get<Unit>().AssignedTo == entity);
                            var aUnit = assignedUnit.Get<Unit>();
                            aUnit.AssignedTo = EntityReference.Null;
                        });
                    }

                    Raylib.DrawTextEx(VillageIdleEngine.Instance.Font, unitsInRole, new Vector2(260, yStart + yIndex * yIncrement), 24, 0f, Color.Black);

                    if (UiHelpers.DrawImageAsButton(TextureKey.ArrowSilverUp, new Vector2(300, yStart + yIndex * yIncrement + 5), availableUnits.Count <= 0))
                    {
                        var nextUnit = availableUnits.FirstOrDefault();
                        if (nextUnit != default)
                        {
                            var producer = world.Create(ProducerStore.Producer);
                            producer.Set(new ProductionUnit { Producer = production.Key });
                            var render = new Render(TextureKey.MedievalSpriteSheet);
                            render.SetSource(SpriteSheetStore.Instance.GetTileSheetSource(SpriteKey.BigFarm));
                            producer.Set(render);

                            nextUnit.Get<Unit>().AssignedTo = producer.Reference();
                        }
                    }
                    yIndex++;
                }
            });
        }

    }
}
