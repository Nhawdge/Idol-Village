using IdolVillage;
using IdolVillage.Utilities;
using Raylib_cs;
using System.Numerics;
using System.Reflection.PortableExecutable;

namespace IdolVillage.Scenes.World1.Helpers
{
    internal class UiHelpers
    {
        private static readonly int SideBarWidth = 350;
        private static readonly int SideBarPadding = 10;
        private static readonly int SideBarInnerWidth = 330;
        private static string toolTipText = "";

        internal static void DrawTextWithBackground(TextureKey textureKey, string text, Vector2 position, bool centered = false)
        {
            var patch = TextureManager.Instance.NPatchInfos[textureKey];
            var texture = TextureManager.Instance.GetTexture(textureKey);

            patch.Source = new Rectangle(0, 0, texture.Width, texture.Height);

            var size = Raylib.MeasureTextEx(IdolVillageEngine.Instance.Font, text, 24, 0);
            var rect = new Rectangle((int)position.X, (int)position.Y, SideBarInnerWidth, size.Y);

            position = new Vector2((int)(rect.X + 10), (int)(rect.Y + rect.Height / 2 - size.Y / 2));
            if (centered)
                position = new Vector2((int)(rect.X + rect.Width / 2 - size.X / 2), (int)(rect.Y + rect.Height / 2 - size.Y / 2));

            rect.Height *= 1.35f;
            Raylib.DrawTextureNPatch(texture, patch, rect, Vector2.Zero, 0f, Color.White);
            Raylib.DrawTextEx(IdolVillageEngine.Instance.Font, text, position, 24, 0f, Color.Black);
        }

        internal static bool DrawButtonWithBackground(TextureKey textureKey, string text, Vector2 position, string toolTip, bool isDisabled = false, bool centered = false)
        {
            var mousePos = Raylib.GetMousePosition();


            var color = Color.White;
            var isClicked = false;

            var patch = TextureManager.Instance.NPatchInfos[textureKey];
            var texture = TextureManager.Instance.GetTexture(textureKey);

            patch.Source = new Rectangle(0, 0, texture.Width, texture.Height);

            var size = Raylib.MeasureTextEx(IdolVillageEngine.Instance.Font, text, 24, 0);
            var rect = new Rectangle((int)position.X, (int)position.Y, SideBarInnerWidth, size.Y);

            position = new Vector2((int)(rect.X + 10), (int)(rect.Y + rect.Height / 2 - size.Y / 2));
            if (centered)
                position = new Vector2((int)(rect.X + rect.Width / 2 - size.X / 2), (int)(rect.Y + rect.Height / 2 - size.Y / 2));

            rect.Height *= 1.35f;

            if (Raylib.CheckCollisionPointRec(mousePos, rect))
            {
                if (!string.IsNullOrEmpty(toolTip))
                {
                    toolTipText = toolTip;
                }
                color = Color.Gray;
                if (InteractionHelper.GetMouseClick(MouseButton.Left))
                {
                    color = Color.DarkGray;
                    isClicked = true;
                }
            }
            if (isDisabled)
            {
                isClicked = false;
                color = Color.Red;
            }

            Raylib.DrawTextureNPatch(texture, patch, rect, Vector2.Zero, 0f, color);
            Raylib.DrawTextEx(IdolVillageEngine.Instance.Font, text, position, 24, 0f, Color.Black);

            return isClicked;
        }

        internal static bool DrawImageAsButton(TextureKey textureKey, Vector2 position, bool isDisabled = false)
        {
            var mousePos = Raylib.GetMousePosition();
            var color = Color.White;
            var isClicked = false;

            var texture = TextureManager.Instance.GetTexture(textureKey);

            var rect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            if (!isDisabled && Raylib.CheckCollisionPointRec(mousePos, rect))
            {
                color = Color.Gray;
                if (InteractionHelper.GetMouseClick(MouseButton.Left))
                {
                    color = Color.DarkGray;
                    isClicked = true;
                }
            }
            if (isDisabled)
            {
                isClicked = false;
                color = Color.Red;
            }
            Raylib.DrawTextureEx(texture, position, 0f, 1f, color);
            return isClicked;
        }

        internal static void DrawToolTipOnMouse()
        {
            if (string.IsNullOrEmpty(toolTipText)) return;
            var textureKey = TextureKey.BlueBox;
            var text = toolTipText;
            var textSize = 24;
            var position = Raylib.GetMousePosition();
            position.X += 10;
            position.Y += 10;

            var patch = TextureManager.Instance.NPatchInfos[textureKey];
            var texture = TextureManager.Instance.GetTexture(textureKey);

            patch.Source = new Rectangle(0, 0, texture.Width, texture.Height);

            var size = Raylib.MeasureTextEx(IdolVillageEngine.Instance.Font, text, textSize, 0);
            var rect = new Rectangle((int)position.X, (int)position.Y, SideBarInnerWidth, size.Y);

            position = new Vector2((int)(rect.X + 10), (int)(rect.Y + rect.Height / 2 - size.Y / 2));

            //rect.Height *= 1.3f;
            rect.Height += size.Y / 24 * 8;
            toolTipText = string.Empty;

            Raylib.DrawTextureNPatch(texture, patch, rect, Vector2.Zero, 0f, Color.White);
            Raylib.DrawTextEx(IdolVillageEngine.Instance.Font, text, position, textSize, 0f, Color.Black);
        }
    }
}
