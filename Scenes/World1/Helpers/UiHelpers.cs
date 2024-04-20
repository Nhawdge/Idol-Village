using Raylib_cs;
using System.Numerics;
using VillageIdle.Utilities;

namespace VillageIdle.Scenes.World1.Helpers
{
    internal class UiHelpers
    {
        private static readonly int SideBarWidth = 350;
        private static readonly int SideBarPadding = 10;
        private static readonly int SideBarInnerWidth = 330;

        internal static void DrawTextWithBackground(TextureKey textureKey, string text, Vector2 position, bool centered = false)
        {
            var patch = TextureManager.Instance.NPatchInfos[textureKey];
            var texture = TextureManager.Instance.GetTexture(textureKey);

            patch.Source = new Rectangle(0, 0, texture.Width, texture.Height);

            var size = Raylib.MeasureTextEx(VillageIdleEngine.Instance.Font, text, 24, 0);
            var rect = new Rectangle((int)position.X, (int)position.Y, SideBarInnerWidth, size.Y);

            position = new Vector2((int)(rect.X + 10), (int)(rect.Y + (rect.Height / 2) - (size.Y / 2)));
            if (centered)
                position = new Vector2((int)(rect.X + (rect.Width / 2) - (size.X / 2)), (int)(rect.Y + (rect.Height / 2) - (size.Y / 2)));

            rect.Height *= 1.35f;
            Raylib.DrawTextureNPatch(texture, patch, rect, Vector2.Zero, 0f, Color.White);
            Raylib.DrawTextEx(VillageIdleEngine.Instance.Font, text, position, 24, 0f, Color.Black);
        }

        internal static bool DrawButtonWithBackground(TextureKey textureKey, string text, Vector2 position, bool isDisabled = false, bool centered = false)
        {
            var mousePos = Raylib.GetMousePosition();

            var color = Color.White;
            var isClicked = false;

            var patch = TextureManager.Instance.NPatchInfos[textureKey];
            var texture = TextureManager.Instance.GetTexture(textureKey);

            patch.Source = new Rectangle(0, 0, texture.Width, texture.Height);

            var size = Raylib.MeasureTextEx(VillageIdleEngine.Instance.Font, text, 24, 0);
            var rect = new Rectangle((int)position.X, (int)position.Y, SideBarInnerWidth, size.Y);

            position = new Vector2((int)(rect.X + 10), (int)(rect.Y + (rect.Height / 2) - (size.Y / 2)));
            if (centered)
                position = new Vector2((int)(rect.X + (rect.Width / 2) - (size.X / 2)), (int)(rect.Y + (rect.Height / 2) - (size.Y / 2)));

            rect.Height *= 1.35f;

            if (!isDisabled && Raylib.CheckCollisionPointRec(mousePos, rect))
            {
                color = Color.Gray;
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    color = Color.DarkGray;
                    isClicked = true;
                }
            }
            if (isDisabled)
                color = Color.Red;

            Raylib.DrawTextureNPatch(texture, patch, rect, Vector2.Zero, 0f, color);
            Raylib.DrawTextEx(VillageIdleEngine.Instance.Font, text, position, 24, 0f, Color.Black);


            return isClicked;
        }

        internal static bool DrawImageAsButton(TextureKey textureKey, Vector2 position, bool isDisabled =false)
        {
            var mousePos = Raylib.GetMousePosition();
            var color = Color.White;
            var isClicked = false;

            var texture = TextureManager.Instance.GetTexture(textureKey);

            var rect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            if (!isDisabled && Raylib.CheckCollisionPointRec(mousePos, rect))
            {
                color = Color.Gray;
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    color = Color.DarkGray;
                    isClicked = true;
                }
            }
            if (isDisabled)
                color = Color.Red;
            Raylib.DrawTextureEx(texture, position, 0f, 1f, color);
            return isClicked;
        }
    }
}
