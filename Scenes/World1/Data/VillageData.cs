using System.ComponentModel;

namespace VillageIdle.Scenes.World1.Data
{
    internal class VillageData
    {
        private VillageData() { }
        internal static VillageData Instance = new();

        internal float TotalPrestige = 1f;
        internal float TotalPopulation = 0f;

        // Raw Resources
        internal float Food = 0f;
        internal float Wood = 0f;
        internal float Wool = 0f;
        internal float MetalOre = 0f;
        internal float Stone = 0f;

        // Refined Resources
        internal float Meals = 0f;
        internal float Lumber = 0f;
        internal float Cloth = 0f;
        internal float Metal = 0f;

        internal float Gold = 0f;
        internal float Leather = 0f;
        internal float Tools = 0f;

    }
}
