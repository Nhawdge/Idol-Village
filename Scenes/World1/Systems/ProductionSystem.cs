using Arch.Core;
using Arch.Core.Extensions;
using IdolVillage;
using IdolVillage.Scenes.Components;
using IdolVillage.Scenes.World1.Data;
using Raylib_cs;

namespace IdolVillage.Scenes.World1.Systems
{
    internal class ProductionSystem : GameSystem
    {
        internal override void Update(World world)
        {
            var query = new QueryDescription().WithAll<ProductionUnit>();
            var unitQuery = new QueryDescription().WithAll<Render, Unit>();

            world.Query(in query, (entity) =>
            {
                var assignedUnits = new List<Entity>();
                world.GetEntities(in unitQuery, assignedUnits);

                assignedUnits = assignedUnits.Where(x =>
                    x.Get<Unit>().AssignedTo == entity
                    && x.Get<Unit>().CurrentAction == Unit.UnitActions.Work).ToList();

                var productionUnit = entity.Get<ProductionUnit>();
                var producer = ProducerStore.Instance.Producers[productionUnit.Producer];
                var render = entity.Get<Render>();

                var canProduce = false;

                if (productionUnit.InProduction)
                {
                    canProduce = true;
                }
                else if (producer.ProductionCost?.Count > 0)
                {
                    var canAfford = ProducerStore.Instance.CanProducerProduce(producer.Key);
                    
                    canProduce = canAfford;

                    if (canAfford)
                    {
                        foreach (var cost in producer.ProductionCost)
                        {
                            VillageData.Instance.Resources[cost.Key] -= cost.Value;
                        }
                    }
                }
                else { canProduce = true; }

                if (canProduce)
                {
                    productionUnit.InProduction = true;
                    productionUnit.CurrentProduction += Raylib.GetFrameTime() * producer.ProducedPerSecond * assignedUnits.Count;
                }

                // Finished
                if (productionUnit.CurrentProduction >= producer.ProductionRequired)
                {
                    productionUnit.CurrentProduction -= producer.ProductionRequired;
                    VillageData.Instance.AddResource(producer.Resource, producer.ResourceAmountProduced);
                    productionUnit.InProduction = false;
                }

                var color = Color.Red;

                var percentDone = productionUnit.CurrentProduction / producer.ProductionRequired;
                Raylib.DrawRing(render.Position with { Y = render.Position.Y - 75 }, 5, 15, 270, 270 + 360 * percentDone, 100, color);
            });
        }
    }
}
