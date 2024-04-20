namespace VillageIdle.Scenes.World1.Data
{
    internal class VillageData
    {
        private VillageData()
        {
            foreach (Resource resource in Enum.GetValues(typeof(Resource)))
            {
                Resources.Add(resource, 0);
            }
        }
        internal static VillageData Instance = new();

        internal float TotalPrestige = 1f;
        internal float TotalPopulation = 0f;

        internal Dictionary<Resource, double> Resources = new();
    }

    public enum Resource
    {
        None,
        Food,
        Wood,
        Wool,
        MetalOre,
        Stone,
        Meals,
        Lumber,
        Cloth,
        Metal,
        Gold,
        Leather,
        Tools,
        BuildingMaterial,
    }
}
