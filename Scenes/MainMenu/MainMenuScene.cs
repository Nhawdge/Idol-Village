using VillageIdle.Scenes.MainMenu.Components;
using VillageIdle.Scenes.MainMenu.Systems;

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
                    //VillageIdleEngine.Instance.ActiveScene = new GameScene();
                },
                Text = "Start Game",
            }) ;
        }
    }
}
