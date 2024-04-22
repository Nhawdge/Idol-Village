namespace IdolVillage.Scenes.World1.Data
{
    internal class VillageData
    {
        private VillageData()
        {
            foreach (Resource resource in Enum.GetValues(typeof(Resource)))
            {
                Resources.Add(resource, -1);
            }
        }

        internal void AddResource(Resource resource, double amount)
        {
            if (Resources.ContainsKey(resource))
            {
                Resources[resource] += amount;
            }
            else
            {
                Resources.Add(resource, amount);
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
        Veggies,
        Protien,
        Pelt,
        Leather,
        Grain,
        Flour,
        Wood,
        Lumber,
        BuildingMaterial,
        Tools,
        GoldOre,
        Coins,
        Stone,
        StoneBuildingMaterials,
        MetalOre,
        Metal,
        Meals,
        Wool,
        Cloth,
    }
}
