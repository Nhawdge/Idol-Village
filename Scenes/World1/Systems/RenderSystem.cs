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
            var query1 = new QueryDescription().WithAll<UnderLayer>();
            world.Query(in query1, (entity) =>
            {
                if (entity.Has<Render>())
                {
                    var render = entity.Get<Render>();
                    render.Draw();
                }
                else if (entity.Has<Sprite>())
                {
                    var render = entity.Get<Sprite>();
                    render.Draw();
                }
            });
            var query2 = new QueryDescription().WithAll<GroundLayer>();
            world.Query(in query2, (entity) =>
            {
                if (entity.Has<Render>())
                {
                    var render = entity.Get<Render>();
                    render.Draw();
                }
                else if (entity.Has<Sprite>())
                {
                    var render = entity.Get<Sprite>();
                    render.Draw();
                }
            });
            var query3 = new QueryDescription().WithAll<StructureLayer>();
            world.Query(in query3, (entity) =>
            {
                if (entity.Has<Render>())
                {
                    var render = entity.Get<Render>();
                    render.Draw();
                }
                else if (entity.Has<Sprite>())
                {
                    var render = entity.Get<Sprite>();
                    render.Draw();
                }
            });
            var query4 = new QueryDescription().WithAll<UnitLayer>();
            world.Query(in query4, (entity) =>
            {
                if (entity.Has<Render>())
                {
                    var render = entity.Get<Render>();
                    render.Draw();
                }
                else if (entity.Has<Sprite>())
                {
                    var render = entity.Get<Sprite>();
                    render.Draw();
                }
            });
            var query5 = new QueryDescription().WithAll<SkyLayer>();
            world.Query(in query5, (entity) =>
            {
                if (entity.Has<Render>())
                {
                    var render = entity.Get<Render>();
                    render.Draw();
                }
                else if (entity.Has<Sprite>())
                {
                    var render = entity.Get<Sprite>();
                    render.Draw();
                }
            });
        }
    }
}
