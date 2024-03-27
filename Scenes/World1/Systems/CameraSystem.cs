using Arch.Core;
using Raylib_cs;

namespace VillageIdle.Scenes.World1.Systems
{
    internal class CameraSystem : GameSystem
    {
        internal override void Update(World world)
        {
            var factor = 10;
            if (Raylib.IsKeyDown(KeyboardKey.LeftShift))
            {
                factor = 20;
            }
            var zoomSpeed = Math.Max((1 - VillageIdleEngine.Instance.Camera.Zoom) * factor, 1);
            if (Raylib.IsKeyDown(KeyboardKey.W))
            {
                VillageIdleEngine.Instance.Camera.Target.Y -= 5 * zoomSpeed;
            }
            if (Raylib.IsKeyDown(KeyboardKey.S))
            {
                VillageIdleEngine.Instance.Camera.Target.Y += 5 * zoomSpeed;
            }
            if (Raylib.IsKeyDown(KeyboardKey.A))
            {
                VillageIdleEngine.Instance.Camera.Target.X -= 5 * zoomSpeed;
            }
            if (Raylib.IsKeyDown(KeyboardKey.D))
            {
                VillageIdleEngine.Instance.Camera.Target.X += 5 * zoomSpeed;
            }

            var scroll = Raylib.GetMouseWheelMove();
            if (scroll < 0)
            {
                VillageIdleEngine.Instance.Camera.Zoom = Math.Max(VillageIdleEngine.Instance.Camera.Zoom - 0.2f, 0.2f);
            }
            else if (scroll > 0)
            {
                VillageIdleEngine.Instance.Camera.Zoom += 0.2f;
            }
        }
    }
}
