using VillageIdle.Utilities;

namespace VillageIdle.Scenes.MainMenu.Components
{
    internal class UiButton : UiTitle
    {
        internal TextureKey Background;

        internal Action Action { get; set; } = () => { };
    }
}
