using Arch.Core;
using Arch.Core.Extensions;
using IdolVillage.Extensions;
using IdolVillage.Scenes.MainMenu.Components;
using IdolVillage.Utilities;
using Raylib_cs;
using System.Numerics;

namespace IdolVillage.Scenes.MainMenu.Systems
{
    internal class MenuSystem : GameSystem
    {
        internal override void Update(World world) { }

        internal override void UpdateNoCamera(World world)
        {
            if (IdolVillageEngine.Instance.ActiveScene is MainMenuScene parent)
            {
                parent.LoadingTime += Raylib.GetFrameTime();

                if (parent.LoadingTime > 10)
                {
                    var whiteWithFade = Raylib.Fade(Color.White, (parent.LoadingTime - 10) / 2);

                    var mousePosition = Raylib.GetMousePosition();
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
                    Raylib.DrawTextureNPatch(backgroundTexture, patch, new Rectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight()), Vector2.Zero, 0f, whiteWithFade);

                    if (IdolVillageEngine.Instance.ActiveScene is MainMenuScene mainMenu)
                    {
                        mainMenu.Logo.Draw();
                    }

                    var query = new QueryDescription().WithAny<UiTitle, UiButton, SpriteButton, UiSlider>();
                    var uiElementCount = world.CountEntities(in query);

                    var centerPoint = new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2);

                    var placementContainer = new Rectangle(centerPoint.X - 300, centerPoint.Y - 300, 600, 500);

                    var container = world.QueryFirstOrNull<UiContainer>();
                    if (container.HasValue)
                    {
                        var uiContainer = container.Value.Get<UiContainer>();
                    }

                    world.Query(in query, (entity) =>
                    {
                        if (entity.Has<UiTitle>())
                        {
                            var titleComponent = entity.Get<UiTitle>();

                            var text = titleComponent.Text;
                            var rect = new Rectangle(centerPoint.X - 100, 0 + 50 * titleComponent.Order, 200, 100);
                        }

                        if (entity.Has<UiButton>())
                        {
                            var button = entity.Get<UiButton>();
                            var rect = placementContainer with
                            {
                                X = placementContainer.X + placementContainer.Width / 2 - 200 / 2,
                                Y = placementContainer.Y + 60 * button.Order,
                                Width = 200,
                                Height = 50
                            };

                            var boxColor = Color.White;
                            if (Raylib.CheckCollisionPointRec(mousePosition, rect))
                            {
                                boxColor = Color.LightGray;
                                if (Raylib.IsMouseButtonDown(MouseButton.Left))
                                {
                                    boxColor = Color.DarkGray;
                                }
                                if (Raylib.IsMouseButtonReleased(MouseButton.Left))
                                {
                                    button.Action();
                                }
                            }

                            var background = TextureManager.Instance.GetTexture(button.Background);
                            patch.Source = new Rectangle(0, 0, background.Width, background.Height);
                            Raylib.DrawTextureNPatch(background, patch, rect, Vector2.Zero, 0f, boxColor);
                            var size = Raylib.MeasureTextEx(IdolVillageEngine.Instance.Font, button.Text, 20, 0);
                            var position = new Vector2((int)(rect.X + rect.Width / 2 - size.X / 2), (int)rect.Y + rect.Height / 2 - size.Y / 2);
                            Raylib.DrawTextEx(IdolVillageEngine.Instance.Font, button.Text, position, 20, 0, Color.Black);
                        }
                    });
                }
                else
                {
                    if (InteractionHelper.GetMouseClick(MouseButton.Left))
                    {
                        parent.LoadingTime = 10;
                    }
                    parent.RaylibLogo.Draw();
                }
            }
        }
    }
}
