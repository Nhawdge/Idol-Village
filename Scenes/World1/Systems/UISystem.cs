using Arch.Core;
using Arch.Core.Extensions;
using IdolVillage.Scenes.Components;
using IdolVillage.Scenes.World1.Data;
using IdolVillage.Scenes.World1.Helpers;
using IdolVillage.Utilities;
using Raylib_cs;
using System.Numerics;

namespace IdolVillage.Scenes.World1.Systems
{
    internal class UISystem : GameSystem
    {
        private static readonly int SideBarWidth = 350;
        private static readonly int SideBarPadding = 10;
        private static readonly int SideBarInnerWidth = SideBarWidth - SideBarPadding * 2;
        private int yStartBase = 10;

        internal override void Update(World world) { }

        internal override void UpdateNoCamera(World world)
        {
            var producerQuery = new QueryDescription().WithAll<ProductionUnit>();

            var unitQuery = new QueryDescription().WithAll<Unit>();

            var availableUnits = new List<EntityReference>();
            var totalUnits = 0;
            var allUnits = new List<EntityReference>();

            if (Raylib.IsKeyDown(KeyboardKey.PageDown))
            {
                yStartBase = Math.Min(yStartBase - 10, 1000);
            }
            if (Raylib.IsKeyDown(KeyboardKey.PageUp))
            {
                yStartBase = Math.Min(yStartBase + 10, 10);
            }

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

            var query = new QueryDescription().WithAll<Interactable>();
            var mousePos = Raylib.GetMousePosition();
            world.Query(in query, (entity) =>
            {
                if (entity.Id != Singleton.Instance.SelectedUnit)
                    return;

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
                var yStart = 10 + yStartBase;
                var yIncrement = 35;

                var backgroundTexture = TextureManager.Instance.GetTexture(TextureKey.BrownBox);

                // UI Panel
                patch.Source = new Rectangle(0, 0, backgroundTexture.Width, backgroundTexture.Height);
                Raylib.DrawTextureNPatch(backgroundTexture, patch, new Rectangle(0, 0, SideBarWidth, Raylib.GetScreenHeight()), Vector2.Zero, 0f, Color.White);

                var text = interactable.Name;
                var size = Raylib.MeasureTextEx(IdolVillageEngine.Instance.Font, text, 24, 0);
                var position = new Vector2(10, yStart + yIndex++ * yIncrement);
                var rect = new Rectangle(10, 10, SideBarInnerWidth, 40);

                if (!string.IsNullOrEmpty(interactable.Name))
                {
                    // Title
                    UiHelpers.DrawTextWithBackground(TextureKey.BlueBox, text, position, true);
                }

                if (interactable.ShowResources)
                {
                    // Resources
                    var resourcesWithValues = VillageData.Instance.Resources.Where(x => x.Value >= 0);
                    if (resourcesWithValues.Any())
                    {
                        text = string.Join("\n", resourcesWithValues.Select(x => $"{x.Key}: {x.Value}"));

                        UiHelpers.DrawTextWithBackground(TextureKey.BlueBox, text, new Vector2(10, yStart + yIndex++ * yIncrement));
                        size = Raylib.MeasureTextEx(IdolVillageEngine.Instance.Font, text, 30, 0);
                        yStart += (int)size.Y + 30;
                    }
                }

                if (interactable.ShowUnits)
                {
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
                            VillageData.Instance.ApplyCosts(UnitStore.Instance.UnitCosts[unit]);
                        }
                        yIndex++;
                    }
                }

                if (interactable.ShowResearch)
                {
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

                        var costString = $"Costs:\n{string.Join("\n", research.Costs.Select(x => $"{x.Key}: {x.Value}"))}\n{research.Description}";
                        if (UiHelpers.DrawButtonWithBackground(TextureKey.BlueBox, $"{research.Name}", new Vector2(10, yStart + yIndex * yIncrement), costString, !canAfford))
                        {
                            if (!canAfford)
                                continue;

                            VillageData.Instance.ApplyCosts(research.Costs);

                            //pass
                            research.Researched = true;
                            foreach (var producerKey in research.ProductionToAdd)
                            {
                                ProducerStore.Instance.Producers[producerKey].IsAvailable = true;
                            }
                        }
                        yIndex++;
                    }
                }

                if (interactable.ShowProducers)
                {
                    text = $"Jobs - Idle: {availableUnits.Count},  Total: {totalUnits}";
                    size = Raylib.MeasureTextEx(IdolVillageEngine.Instance.Font, text, 24, 0);
                    position = new Vector2(10 + SideBarInnerWidth / 2 - size.X / 2, yStart + yIndex++ * yIncrement);
                    Raylib.DrawTextEx(IdolVillageEngine.Instance.Font, text, position, 24, 0f, Color.Black);

                    foreach (var production in ProducerStore.Instance.GetAvailableProducers())
                    {
                        var canBuild = ProducerStore.Instance.IsProducerBuildable(production.Key);

                        var tooltip = "Costs:\n" + string.Join("\n", production?.BuildCost.Select(x => $"{x.Key}: {x.Value}"));
                        if (UiHelpers.DrawButtonWithBackground(TextureKey.BlueBox, production.Name, new Vector2(10, yStart + yIndex * yIncrement + 5), tooltip, !canBuild))
                        {
                            ProducerStore.CreateProducer(world, production);
                        }
                        yIndex++;
                    }
                }

                if (interactable.ShowAssignments && entity.Has<ProductionUnit>())
                {
                    text = $"Jobs - Idle: {availableUnits.Count}, Total: {totalUnits}";
                    size = Raylib.MeasureTextEx(IdolVillageEngine.Instance.Font, text, 24, 0);
                    position = new Vector2(10 + SideBarInnerWidth / 2 - size.X / 2, yStart + yIndex++ * yIncrement);
                    Raylib.DrawTextEx(IdolVillageEngine.Instance.Font, text, position, 24, 0f, Color.Black);

                    var productionUnit = entity.Get<ProductionUnit>();
                    var production = ProducerStore.Instance.Producers[productionUnit.Producer];

                    UiHelpers.DrawTextWithBackground(TextureKey.BlueBox, production.Name, new Vector2(10, yStart + yIndex * yIncrement));

                    var unitsInRole = allUnits.Where(x => x.Entity.Get<Unit>().AssignedTo == entity).Count();

                    if (UiHelpers.DrawImageAsButton(TextureKey.ArrowSilverDown, new Vector2(200, yStart + yIndex * yIncrement + 5), unitsInRole <= 0))
                    {
                        if (productionUnit.Producer == production.Key)
                        {
                            var assignedUnit = allUnits.FirstOrDefault(x => x.Entity.Get<Unit>().AssignedTo == entity);
                            if (assignedUnit != Entity.Null && assignedUnit.Entity.Has<Unit>())
                            {
                                var aUnit = assignedUnit.Entity.Get<Unit>();
                                aUnit.AssignedTo = EntityReference.Null;
                            }
                        }
                    }

                    Raylib.DrawTextEx(IdolVillageEngine.Instance.Font, unitsInRole.ToString(), new Vector2(260, yStart + yIndex * yIncrement), 24, 0f, Color.Black);
                    var canAssignMore = availableUnits.Count > 0 && unitsInRole < production.WorkerCapacity;


                    if (UiHelpers.DrawImageAsButton(TextureKey.ArrowSilverUp, new Vector2(300, yStart + yIndex * yIncrement + 5), !canAssignMore))
                    {
                        var nextUnit = availableUnits.FirstOrDefault();
                        if (nextUnit != default)
                        {
                            var nextUnitUnit = nextUnit.Entity.Get<Unit>();

                            nextUnitUnit.AssignedTo = entity.Reference();
                            var render = entity.Get<Render>();
                            nextUnitUnit.MovementGoal = render.Position;
                        }
                    }
                    yIndex++;
                }
            });
        }
    }
}
