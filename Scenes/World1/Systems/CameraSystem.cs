using Arch.Core;
using Raylib_cs;

namespace IdolVillage.Scenes.World1.Systems
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
            var zoomSpeed = Math.Max((1 - IdolVillageEngine.Instance.Camera.Zoom) * factor, 1);
            if (Raylib.IsKeyDown(KeyboardKey.W))
            {
                IdolVillageEngine.Instance.Camera.Target.Y -= 5 * zoomSpeed;
            }
            if (Raylib.IsKeyDown(KeyboardKey.S))
            {
                IdolVillageEngine.Instance.Camera.Target.Y += 5 * zoomSpeed;
            }
            if (Raylib.IsKeyDown(KeyboardKey.A))
            {
                IdolVillageEngine.Instance.Camera.Target.X -= 5 * zoomSpeed;
            }
            if (Raylib.IsKeyDown(KeyboardKey.D))
            {
                IdolVillageEngine.Instance.Camera.Target.X += 5 * zoomSpeed;
            }

            var scroll = Raylib.GetMouseWheelMove();
            if (scroll < 0)
            {
                IdolVillageEngine.Instance.Camera.Zoom = Math.Max(IdolVillageEngine.Instance.Camera.Zoom - 0.2f, 0.2f);
            }
            else if (scroll > 0)
            {
                IdolVillageEngine.Instance.Camera.Zoom += 0.2f;
            }
        }
    }
}
