using Arch.Core;
using Arch.Core.Extensions;
using IdolVillage;
using IdolVillage.Extensions;
using IdolVillage.Scenes.Components;

namespace IdolVillage.Scenes.World1.Systems
{
    internal class RenderSystem : GameSystem
    {
        internal int index = 1;
        internal override void Update(World world)
        {
            var query1 = new QueryDescription().WithAll<Render, UnderLayer>();
            world.Query(in query1, (entity) =>
            {
                var render = entity.Get<Render>();
                render.Draw();
            });
            var query2 = new QueryDescription().WithAll<Render, GroundLayer>();
            world.Query(in query2, (entity) =>
            {
                var render = entity.Get<Render>();
                render.Draw();
            });
            var query3 = new QueryDescription().WithAll<Render, StructureLayer>();
            world.Query(in query3, (entity) =>
            {
                var render = entity.Get<Render>();
                render.Draw();
            });
            var query4 = new QueryDescription().WithAll<Render, UnitLayer>();
            world.Query(in query4, (entity) =>
            {
                var render = entity.Get<Render>();
                render.Draw();
            });
            var query5 = new QueryDescription().WithAll<Render, SkyLayer>();
            world.Query(in query5, (entity) =>
            {
                var render = entity.Get<Render>();
                render.Draw();
            });
        }
    }
}
