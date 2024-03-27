using Arch.Core;
using Arch.Core.Extensions;
using Raylib_cs;
using VillageIdle.Scenes.Components;
using VillageIdle.Scenes.World1.Components;

namespace VillageIdle.Scenes.World1.Systems
{
    internal class InteractionSystem : GameSystem
    {
        internal override void Update(World world)
        {
            var query = new QueryDescription().WithAll<Interactable, Render>();
            var mousePos = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), VillageIdleEngine.Instance.Camera);
            world.Query(in query, (entity) =>
            {
                var interactable = entity.Get<Interactable>();
                var render = entity.Get<Render>();
                if (Raylib.CheckCollisionPointRec(mousePos, render.Destination))
                {
                    render.Color = Color.Gray;
                    if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                    {
                        interactable.IsSelected = !interactable.IsSelected;
                    }
                }
                else
                {
                    render.Color = Color.White;
                }

            });
        }
    }
}
