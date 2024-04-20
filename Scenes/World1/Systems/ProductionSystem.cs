using Arch.Core;
using Arch.Core.Extensions;
using Raylib_cs;
using VillageIdle.Scenes.Components;
using VillageIdle.Scenes.World1.Data;
using VillageIdle.Utilities;

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
                if (productionUnit.CurrentProduction > producer.ProductionRequired * .75f)
                {
                    render.SetSource(SpriteSheetStore.Instance.GetTileSheetSource(SpriteKey.BigFarmHarvest));
                }
                else
                {
                    render.SetSource(SpriteSheetStore.Instance.GetTileSheetSource(SpriteKey.BigFarm));
                }
                var color = Color.Red;
                //color = Raylib.ColorAlpha(color, productionUnit.CurrentProduction / producer.ProductionRequired);
                Raylib.DrawCircle((int)render.Position.X, (int)render.Position.Y - 75, 10, color);

                Raylib.DrawRing(render.Position, 10, 15, 0, productionUnit.CurrentProduction / producer.ProductionRequired, 0, color);
            });
        }
    }
}
