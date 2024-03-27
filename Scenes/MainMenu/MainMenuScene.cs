using VillageIdle.Scenes.MainMenu.Components;
using VillageIdle.Scenes.MainMenu.Systems;
using VillageIdle.Scenes.World1;
using VillageIdle.Utilities;

namespace VillageIdle.Scenes.MainMenu
{
    internal class MainMenuScene : BaseScene
    {
        public MainMenuScene()
        {
            Systems.Add(new MenuSystem());
            World.Create(new UiButton
            {
                Order = 2,
                Action = () =>
                {
                    Console.WriteLine("Start Game");
                    VillageIdleEngine.Instance.ActiveScene = new World1Scene();
                },
                Text = "Start Game",
                Background = TextureKey.BlueBox,
            });
        }
    }
}
