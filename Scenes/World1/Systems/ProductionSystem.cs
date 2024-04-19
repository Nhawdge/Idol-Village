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
                var render = entity.Get<Render>();
                productionUnit.CurrentProduction += Raylib.GetFrameTime();

                if (productionUnit.CurrentProduction >= productionUnit.ProductionTotal)
                {
                    productionUnit.CurrentProduction -= productionUnit.ProductionTotal;
                    VillageData.Instance.Resources[productionUnit.Resource] += productionUnit.ProductionValue;
                }
                if (productionUnit.CurrentProduction > productionUnit.ProductionTotal * .75f)
                {
                    render.SetSource(SpriteSheetStore.Instance.GetTileSheetSource(SpriteKey.BigFarmHarvest));
                }
                else
                {
                    render.SetSource(SpriteSheetStore.Instance.GetTileSheetSource(SpriteKey.BigFarm));
                }
                var color = Color.Red;
                color = Raylib.ColorAlpha(color, productionUnit.CurrentProduction / productionUnit.ProductionTotal);
                Raylib.DrawCircle((int)render.Position.X, (int)render.Position.Y-75, 10, color);
            });
        }
    }
}
