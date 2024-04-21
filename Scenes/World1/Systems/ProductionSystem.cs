using Arch.Core;
using Arch.Core.Extensions;
using Raylib_cs;
using VillageIdle.Scenes.Components;
using VillageIdle.Scenes.World1.Data;

namespace VillageIdle.Scenes.World1.Systems
{
    internal class ProductionSystem : GameSystem
    {
        internal override void Update(World world)
        {
            var query = new QueryDescription().WithAll<ProductionUnit>();

            world.Query(in query, (entity) =>
            {
                var productionUnit = entity.Get<ProductionUnit>();
                var producer = ProducerStore.Instance.Producers[productionUnit.Producer];

                var render = entity.Get<Render>();
                productionUnit.CurrentProduction += Raylib.GetFrameTime() * producer.ProducedPerSecond;

                if (productionUnit.CurrentProduction >= producer.ProductionRequired)
                {
                    productionUnit.CurrentProduction -= producer.ProductionRequired;
                    VillageData.Instance.Resources[producer.Resource] += producer.ResourceAmountProduced;
                }

                var color = Color.Red;

                var percentDone = productionUnit.CurrentProduction / producer.ProductionRequired;
                Raylib.DrawRing(render.Position with { Y = render.Position.Y - 75 }, 5, 15, 270, 270 + 360 * percentDone, 100, color);
            });
        }
    }
}
