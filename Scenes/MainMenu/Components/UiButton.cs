using IdolVillage.Utilities;

namespace IdolVillage.Scenes.MainMenu.Components
{
    internal class UiButton : UiTitle
    {
        internal TextureKey Background;

        internal Action Action { get; set; } = () => { };
    }
}
