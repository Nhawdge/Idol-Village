using Raylib_cs;

namespace IdolVillage.Scenes.MainMenu.Components
{
    internal class UiTitle
    {
        internal int Order = 0;
        public string Text { get; set; }
        public bool UseOverridePosition { get; set; }
        public Rectangle OverridePosition { get; set; }
    }
}
