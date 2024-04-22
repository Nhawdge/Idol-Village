using IdolVillage.Scenes.Components;

namespace IdolVillage.Scenes.MainMenu.Components
{
    internal class SpriteButton : UiTitle
    {
        internal Sprite ButtonSprite;
        internal Sprite TextSprite;
        internal Action Action { get; set; } = () => { };
    }
}
