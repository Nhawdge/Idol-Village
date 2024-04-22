namespace IdolVillage.Scenes.World1.Data
{
    internal class TechTree
    {
        internal static TechTree Instance = new();

        internal List<Technology> Technologies = new();

        internal List<Technology> GetAvailableTechnologies()
        {
            return Technologies.Where(x => x.Researched == false && x.Prerequisites.All(y => y.Researched)).ToList();
        }

        private TechTree()
        {
            LoadTechData();
        }

        private void LoadTechData()
        {
            Technologies.Add(new Technology
            {
                Name = "Foraging",
                Description = "Unlocks the ability to grow crops.",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new() { },
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.Gathering },
            });

            Technologies.Add(new Technology
            {
                Name = "Hunting",
                Description = "Unlocks the ability to hunt for food.",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new List<Technology> { Technologies[0] },
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.Hunting },
                Costs = new() { { Resource.Veggies, 3 } }
            });

            Technologies.Add(new Technology
            {
                Name = "Farming",
                Description = "Unlocks the ability to farm crops.",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new List<Technology> { Technologies[0], Technologies[1] },
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.Farm },
                Costs = new() {
                    { Resource.Veggies, 10 },
                    { Resource.Protien, 10 },
                }
            });

            Technologies.Add(new Technology
            {
                Name = "Forestry",
                Description = "Unlocks the ability to gain wood.",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new List<Technology> { Technologies[0], Technologies[1] },
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.Wood },
                Costs = new() {
                    { Resource.Veggies, 10 },
                    { Resource.Protien, 10 }
                }
            });

            Technologies.Add(new Technology
            {
                Name = "Lumbermill",
                Description = "Unlocks the Lumber mill.",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new List<Technology> { Technologies[3] },
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.Lumber },
                Costs = new() {
                    { Resource.Wood, 10 }
                }
            });
        }
    }

    internal class Technology
    {
        internal string Name;
        internal string Description;
        internal List<Technology> Prerequisites = new();
        internal bool Researched = false;
        internal float ResearchCost = 0f;
        internal float ResearchTime = 0f;
        internal float ResearchProgress = 0f;
        internal List<ProducerTypes> ProductionToAdd = new();
        internal Dictionary<Resource, int> Costs = new();
    }

    // - gathering - 30% consistent, 5% chance to return toxic berries/flora
    // - villagers impacted by toxic berries may not perform their tasks for 24 hours
    // - foraging - 40% consistent, 10% chance to return toxic mushrooms/fungus
    // - villagers impacted by toxic mushrooms/fungus may revolt against your diety(reducing overall faith) for 24 hours.
    // - hunting - 10% consistent, but on successful hunt, returned food is multiplied by 2-6.  5% chance that the hunter dies on the hunt and does not return.
    // - trapping - 15% consistent, the first time a trapper goes out, it takes full time and returns with no food.Every time after that, takes half the normal time.
    // - fishing - 20% consistent.  .5 modifier on yield due to waste.  Fishing waste directly contributes benefically to agriculture (when implemented)
}
