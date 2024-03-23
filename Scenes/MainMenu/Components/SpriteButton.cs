using VillageIdle.Scenes.Components;

namespace VillageIdle.Scenes.MainMenu.Components
{
    internal class SpriteButton : UiTitle
    {
        internal Sprite ButtonSprite;
        internal Sprite TextSprite;
        internal Action Action { get; set; } = () => { };
    }
}
