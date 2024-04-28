using Arch.Core;
using Arch.Core.Extensions;
using IdolVillage;
using IdolVillage.Scenes.Components;
using IdolVillage.Scenes.World1.Data;
using IdolVillage.Utilities;
using Raylib_cs;

namespace IdolVillage.Scenes.World1.Systems
{
    internal class InteractionSystem : GameSystem
    {
        internal override void Update(World world)
        {
            var query = new QueryDescription().WithAll<Interactable>();
            var mousePos = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), IdolVillageEngine.Instance.Camera);
            world.Query(in query, (entity) =>
            {
                var interactable = entity.Get<Interactable>();

                Render render;
                if (entity.Has<Render>())
                {
                    render = entity.Get<Render>();
                }
                else //if (entity.Has<Sprite>())
                {
                    render = entity.Get<Sprite>();
                }

                if (Raylib.CheckCollisionPointRec(mousePos, render.CollisionDestination))
                {
                    //Raylib.DrawRectangleLines((int)render.CollisionDestination.X, (int)render.CollisionDestination.Y, (int)render.CollisionDestination.Width, (int)render.CollisionDestination.Height, Color.Gold);
                    render.Color = Color.Gray;
                    if (InteractionHelper.GetMouseClick(MouseButton.Left))
                    {
                        if (Singleton.Instance.SelectedUnit == entity.Id)
                            Singleton.Instance.SelectedUnit = -1;
                        else
                            Singleton.Instance.SelectedUnit = entity.Id;
                        if (IdolVillageEngine.Instance.ActiveScene.Systems.FirstOrDefault(x => x is UISystem) is UISystem uiSystem)
                        {
                            uiSystem.yStartBase = 0;
                        }
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
