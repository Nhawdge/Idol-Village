using IdolVillage.Scenes.Components;
using IdolVillage.Scenes.MainMenu.Components;
using IdolVillage.Scenes.MainMenu.Systems;
using IdolVillage.Scenes.World1;
using IdolVillage.Utilities;
using Raylib_cs;
using System.Numerics;

namespace IdolVillage.Scenes.MainMenu
{
    internal class MainMenuScene : BaseScene
    {
        internal Render Logo;

        public MainMenuScene()
        { 
            Systems.Add(new MenuSystem());

            Logo = new Render(TextureKey.MainLogo);
            Logo.Position = new Vector2(Raylib.GetScreenWidth() / 2, 100);

            World.Create(new UiButton
            {
                Order = 2,
                Action = () =>
                {
                    Console.WriteLine("Start Game");
                    IdolVillageEngine.Instance.ActiveScene = new World1Scene();
                },
                Text = "Start Game",
                Background = TextureKey.BlueBox,
            });

            var logoRender = new Render(TextureKey.MainLogo);
            logoRender.OriginPos = Render.OriginAlignment.Center;
            logoRender.Position = new Vector2(10, 30);
            World.Create(logoRender, new SkyLayer());
        }
    }
}
