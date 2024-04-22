using IdolVillage.Scenes.MainMenu.Components;
using IdolVillage.Scenes.MainMenu.Systems;
using IdolVillage.Scenes.World1;
using IdolVillage.Utilities;

namespace IdolVillage.Scenes.MainMenu
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
                    IdolVillageEngine.Instance.ActiveScene = new World1Scene();
                },
                Text = "Start Game",
                Background = TextureKey.BlueBox,
            });
        }
    }
}
