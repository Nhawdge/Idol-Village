namespace IdolVillage.Scenes.World1.Data
{
    internal class Singleton
    {
        private Singleton() { }
        internal static Singleton Instance = new();

        internal int SelectedUnit = -1;
    }
}
