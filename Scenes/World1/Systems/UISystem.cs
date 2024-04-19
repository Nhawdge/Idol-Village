using Arch.Core;
using Arch.Core.Extensions;
using Raylib_cs;
using System.Numerics;
using VillageIdle.Scenes.Components;
using VillageIdle.Scenes.World1.Data;
using VillageIdle.Utilities;

namespace VillageIdle.Scenes.World1.Systems
{
    internal class UISystem : GameSystem
    {
        internal override void Update(World world) { }
        internal override void UpdateNoCamera(World world)
        {
            var query = new QueryDescription().WithAny<Interactable>();
            var mousePos = Raylib.GetMousePosition();
            world.Query(in query, (entity) =>
            {
                if (entity.Id == Singleton.Instance.SelectedUnit)
                {
                    var interactable = entity.Get<Interactable>();
                    var patch = new NPatchInfo
                    {
                        Left = 10,
                        Top = 10,
                        Right = 10,
                        Bottom = 10,
                        Layout = NPatchLayout.NinePatch
                    };

                    var backgroundTexture = TextureManager.Instance.GetTexture(TextureKey.BrownBox);

                    patch.Source = new Rectangle(0, 0, backgroundTexture.Width, backgroundTexture.Height);
                    Raylib.DrawTextureNPatch(backgroundTexture, patch, new Rectangle(0, 0, 250, Raylib.GetScreenHeight()), Vector2.Zero, 0f, Color.White);

                    var text = "Pavekstan";
                    var rect = new Rectangle(10, 10, 230, 40);
                    var size = Raylib.MeasureTextEx(VillageIdleEngine.Instance.Font, text, 24, 0);
                    var position = new Vector2((int)(rect.X + (rect.Width / 2) - (size.X / 2)), (int)rect.Y + rect.Height / 2 - (size.Y / 2));
                    Raylib.DrawTextureNPatch(TextureManager.Instance.GetTexture(TextureKey.BlueBox), patch, rect, Vector2.Zero, 0f, Color.White);
                    Raylib.DrawTextEx(VillageIdleEngine.Instance.Font, text, position, 24, 0f, Color.Black);

                    text = string.Join("\n", VillageData.Instance.Resources.Select(x => $"{x.Key}: {x.Value}"));
                    DrawTextWithBackground(TextureKey.BlueBox, text, new Vector2(10, 60));


                    foreach (var research in TechTree.Instance.Technologies)
                    {
                        if (DrawButtonWithBackground(TextureKey.BlueBox, research.Name, new Vector2(10, 280)))
                        {
                        }
                    }

                    //text = "Buy Farm";
                    //rect = new Rectangle(10, 280, 230, 40);
                    //size = Raylib.MeasureTextEx(VillageIdleEngine.Instance.Font, text, 24, 0);
                    //position = new Vector2((int)(rect.X + (rect.Width / 2) - (size.X / 2)), (int)rect.Y + rect.Height / 2 - (size.Y / 2));

                    //var color = Color.White;
                    //if (Raylib.CheckCollisionPointRec(mousePos, rect))
                    //{
                    //    color = Color.Gray;
                    //    if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                    //    {
                    //        color = Color.DarkGray;

                    //    }
                    //}

                    //Raylib.DrawTextureNPatch(TitleTexture, patch, rect, Vector2.Zero, 0f, color);
                    //Raylib.DrawTextEx(VillageIdleEngine.Instance.Font, "Buy Farm", position, 24, 0f, Color.Black);

                }
            });
        }

        private static void DrawTextWithBackground(TextureKey textureKey, string text, Vector2 position)
        {
            var patch = TextureManager.Instance.NPatchInfos[textureKey];
            var texture = TextureManager.Instance.GetTexture(textureKey);

            patch.Source = new Rectangle(0, 0, texture.Width, texture.Height);

            var size = Raylib.MeasureTextEx(VillageIdleEngine.Instance.Font, text, 24, 0);
            var rect = new Rectangle((int)position.X, (int)position.Y, 230, size.Y);
            position = new Vector2((int)(rect.X + (rect.Width / 2) - (size.X / 2)), (int)(rect.Y + (rect.Height / 2) - (size.Y / 2)));
            rect.Height *= 1.35f;
            Raylib.DrawTextureNPatch(texture, patch, rect, Vector2.Zero, 0f, Color.White);
            Raylib.DrawTextEx(VillageIdleEngine.Instance.Font, text, position, 24, 0f, Color.Black);
        }

        private static bool DrawButtonWithBackground(TextureKey textureKey, string text, Vector2 position)
        {
            var mousePos = Raylib.GetMousePosition();
            var color = Color.White;
            var isClicked = false;

            var patch = TextureManager.Instance.NPatchInfos[textureKey];
            var texture = TextureManager.Instance.GetTexture(textureKey);

            patch.Source = new Rectangle(0, 0, texture.Width, texture.Height);

            var size = Raylib.MeasureTextEx(VillageIdleEngine.Instance.Font, text, 24, 0);
            var rect = new Rectangle((int)position.X, (int)position.Y, 230, size.Y);
            position = new Vector2((int)(rect.X + (rect.Width / 2) - (size.X / 2)), (int)(rect.Y + (rect.Height / 2) - (size.Y / 2)));
            rect.Height *= 1.35f;
            if (Raylib.CheckCollisionPointRec(mousePos, rect))
            {
                color = Color.Gray;
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    color = Color.DarkGray;
                    isClicked = true;
                }
            }
            Raylib.DrawTextureNPatch(texture, patch, rect, Vector2.Zero, 0f, color);
            Raylib.DrawTextEx(VillageIdleEngine.Instance.Font, text, position, 24, 0f, Color.Black);


            return isClicked;
        }
    }
}
