using Arch.Core;
using Arch.Core.Extensions;
using IdolVillage;
using IdolVillage.Scenes.Components;
using IdolVillage.Scenes.World1.Data;
using IdolVillage.Scenes.World1.Helpers;
using IdolVillage.Utilities;
using Raylib_cs;
using System.Net;
using System.Numerics;

namespace IdolVillage.Scenes.World1.Systems
{
    internal class UISystem : GameSystem
    {
        private static readonly int SideBarWidth = 350;
        private static readonly int SideBarPadding = 10;
        private static readonly int SideBarInnerWidth = SideBarWidth - SideBarPadding * 2;
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

            var availableUnits = new List<EntityReference>();
            var totalUnits = 0;
            var allUnits = new List<EntityReference>();
            world.Query(in unitQuery, (entity) =>
            {
                var unit = entity.Get<Unit>();
                if (!unit.AssignedTo.IsAlive())
                {
                    availableUnits.Add(entity.Reference());
                }
                allUnits.Add(entity.Reference());
                totalUnits++;
            });

            var query = new QueryDescription().WithAll<Interactable>();//.WithAny<Unit, VillageCenter>();
            var mousePos = Raylib.GetMousePosition();
            world.Query(in query, (entity) =>
            {
                if (entity.Id != Singleton.Instance.SelectedUnit)
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

                var yIndex = 0;
                var yStart = 10;
                var yIncrement = 35;

                var backgroundTexture = TextureManager.Instance.GetTexture(TextureKey.BrownBox);

                // UI Panel
                patch.Source = new Rectangle(0, 0, backgroundTexture.Width, backgroundTexture.Height);
                Raylib.DrawTextureNPatch(backgroundTexture, patch, new Rectangle(0, 0, SideBarWidth, Raylib.GetScreenHeight()), Vector2.Zero, 0f, Color.White);

                // Title
                var text = "Pavekstan";
                var rect = new Rectangle(10, 10, SideBarInnerWidth, 40);
                var size = Raylib.MeasureTextEx(IdolVillageEngine.Instance.Font, text, 24, 0);
                var position = new Vector2(10, yStart + yIndex++ * yIncrement);
                UiHelpers.DrawTextWithBackground(TextureKey.BlueBox, text, position, true);

                // Resources
                var resourcesWithValues = VillageData.Instance.Resources.Where(x => x.Value >= 0);
                if (resourcesWithValues.Count() > 0)
                {
                    text = string.Join("\n", resourcesWithValues.Select(x => $"{x.Key}: {x.Value}"));

                    UiHelpers.DrawTextWithBackground(TextureKey.BlueBox, text, new Vector2(10, 60));
                    size = Raylib.MeasureTextEx(IdolVillageEngine.Instance.Font, text, 24, 0);
                    yStart += (int)size.Y + 30;
                }

                // Units
                text = "Units";
                size = Raylib.MeasureTextEx(IdolVillageEngine.Instance.Font, text, 24, 0);
                position = new Vector2(10 + SideBarInnerWidth / 2 - size.X / 2, yStart + yIndex++ * yIncrement);
                Raylib.DrawTextEx(IdolVillageEngine.Instance.Font, text, position, 24, 0f, Color.Black);

                foreach (var unit in UnitStore.AvailableUnits())
                {
                    var unitName = unit.ToString();
                    var isAvailable = UnitStore.Instance.IsUnitAvailable(unit);
                    var costString = "Costs:\n" + string.Join("\n", UnitStore.Instance.UnitCosts[unit].Select(x => $"{x.Key}: {x.Value}"));
                    if (UiHelpers.DrawButtonWithBackground(TextureKey.BlueBox, unitName, new Vector2(10, yStart + yIndex * yIncrement), costString, !isAvailable))
                    {
                        UnitStore.CreateUnit(world, unit);
                        if (isAvailable)
                        {
                            foreach (var cost in UnitStore.Instance.UnitCosts[unit])
                            {
                                VillageData.Instance.Resources[cost.Key] -= cost.Value;
                            }
                        }
                    }
                    yIndex++;
                }

                // Research
                text = "Research";
                size = Raylib.MeasureTextEx(IdolVillageEngine.Instance.Font, text, 24, 0);
                position = new Vector2(10 + SideBarInnerWidth / 2 - size.X / 2, yStart + yIndex++ * yIncrement);
                Raylib.DrawTextEx(IdolVillageEngine.Instance.Font, text, position, 24, 0f, Color.Black);

                foreach (var research in TechTree.Instance.GetAvailableTechnologies())
                {
                    var canAfford = true;
                    if (research.Costs != null)
                    {
                        foreach (var cost in research.Costs)
                        {
                            if (VillageData.Instance.Resources[cost.Key] < cost.Value)
                            {
                                canAfford = false;
                                break;
                            }
                        }
                    }
                    var costString = "Costs:\n" + string.Join("\n", research.Costs.Select(x => $"{x.Key}: {x.Value}"));
                    if (UiHelpers.DrawButtonWithBackground(TextureKey.BlueBox, $"{research.Name}", new Vector2(10, yStart + yIndex * yIncrement), costString, !canAfford))
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
                size = Raylib.MeasureTextEx(IdolVillageEngine.Instance.Font, text, 24, 0);
                position = new Vector2(10 + SideBarInnerWidth / 2 - size.X / 2, yStart + yIndex++ * yIncrement);
                Raylib.DrawTextEx(IdolVillageEngine.Instance.Font, text, position, 24, 0f, Color.Black);

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
                                deleted = true;

                                var assignedUnit = allUnits.FirstOrDefault(x => x.Entity.Get<Unit>().AssignedTo == entity);
                                if (assignedUnit != Entity.Null && entity.Has<Unit>())
                                {
                                    var aUnit = assignedUnit.Entity.Get<Unit>();
                                    aUnit.AssignedTo = EntityReference.Null;
                                }
                                world.Destroy(entity);
                            }
                        });
                    }

                    Raylib.DrawTextEx(IdolVillageEngine.Instance.Font, unitsInRole, new Vector2(260, yStart + yIndex * yIncrement), 24, 0f, Color.Black);

                    if (UiHelpers.DrawImageAsButton(TextureKey.ArrowSilverUp, new Vector2(300, yStart + yIndex * yIncrement + 5), availableUnits.Count <= 0))
                    {
                        var nextUnit = availableUnits.FirstOrDefault();
                        if (nextUnit != default)
                        {
                            var producer = world.Create(ProducerStore.Producer);
                            producer.Set(new StructureLayer());
                            producer.Set(new Interactable());
                            producer.Set(new ProductionUnit { Producer = production.Key });
                            var render = new Render(TextureKey.MedievalSpriteSheet);
                            render.SetSource(production.SpriteRect);
                            var worldCenter = new Vector2(50 * 128, 50 * 128);
                            var spread = 100 * totalUnits + 5;
                            render.Position = worldCenter + new Vector2(Random.Shared.Next(-spread, spread), Random.Shared.Next(-spread, spread));
                            producer.Set(render);

                            var nextUnitUnit = nextUnit.Entity.Get<Unit>();

                            nextUnitUnit.AssignedTo = producer.Reference();
                            nextUnitUnit.MovementGoal = render.Position;
                        }
                    }
                    yIndex++;
                }
            });
        }

    }
}
