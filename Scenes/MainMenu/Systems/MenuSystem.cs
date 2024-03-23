using Arch.Core;
using Arch.Core.Extensions;
using Raylib_cs;
using System.Numerics;
using VillageIdle.Extensions;
using VillageIdle.Scenes.MainMenu.Components;
using VillageIdle.Utilities;

namespace VillageIdle.Scenes.MainMenu.Systems
{
    internal class MenuSystem : GameSystem
    {
        internal override void Update(World world) { }

        internal override void UpdateNoCamera(World world)
        {
            var backgroundTexture = TextureManager.Instance.GetTexture(TextureKey.Empty);
            Raylib.DrawTexturePro(backgroundTexture,
                new Rectangle(0, 0, backgroundTexture.Width, backgroundTexture.Height),
                new Rectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight()),
                Vector2.Zero, 0f, Color.White);
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

                    //RayGui.GuiLabel(rect, text);
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

                    //if (RayGui.GuiButton(rect, button.Text))
                    //{
                    //    button.Action();
                    //}
                }
                //if (entity.Has<SpriteButton>())
                //{
                //    var button = entity.Get<SpriteButton>();

                //    button.ButtonSprite.Play("Normal");
                //    button.TextSprite.Play(button.Text);
                //    if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), button.ButtonSprite.Destination))
                //    {
                //        button.ButtonSprite.Play("Hover");
                //        button.TextSprite.Play($"{button.Text}Hover");
                //        if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
                //        {
                //            button.Action();
                //        }
                //    }
                //    button.ButtonSprite.Draw();
                //    button.TextSprite.Draw();
                //}
                //if (entity.Has<UiSlider>())
                //{
                //    var slider = entity.Get<UiSlider>();
                //    var val = RayGui.GuiSlider(placementContainer with
                //    {
                //        x = placementContainer.x + placementContainer.width / 2 - 200 / 2,
                //        y = placementContainer.y + 60 * slider.Order,
                //        width = 200,
                //        height = 50
                //    },
                //    slider.Text,
                //    SettingsManager.Instance.Settings[slider.SettingKey].ToString("0") + "%",
                //    SettingsManager.Instance.Settings[slider.SettingKey],
                //    slider.MinValue, slider.MaxValue);

                //    SettingsManager.Instance.Settings[slider.SettingKey] = val;

                //}
            });

            var mousePosition = Raylib.GetMousePosition();
            var boxTexture = TextureManager.Instance.GetTexture(TextureKey.BlueBox);
            var boxRect = new Rectangle(0, 0, boxTexture.Width, boxTexture.Height);
            var patch = new NPatchInfo
            {
                Source = boxRect,
                Left = 10,
                Top = 10,
                Right = 10,
                Bottom = 10,
                Layout = NPatchLayout.NinePatch
            };
            Raylib.DrawTextureNPatch(boxTexture, patch, new Rectangle(100, 100, mousePosition.X - 100, mousePosition.Y - 100), Vector2.Zero, 0f, Color.White);

        }
    }
}
