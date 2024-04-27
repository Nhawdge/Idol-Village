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
            Resources[Resource.Belief] = 0;
            Resources[Resource.Veggies] = 10;
        }

        internal void ApplyCosts(Dictionary<Resource, double> costs)
        {
            if (costs == null) return;
            foreach (var cost in costs)
            {
                Resources[cost.Key] -= cost.Value;
            }
        }
        internal void AddResource(Resource resource, double amount)
        {
            if (Resources.ContainsKey(resource))
            {
                if (Resources[resource] == -1)
                {
                    Resources[resource] += 1;
                }
                Resources[resource] += amount;

            }
            else
            {
                Resources.Add(resource, amount + 1);
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
        Belief,
        Veggies,
        Protein,
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
