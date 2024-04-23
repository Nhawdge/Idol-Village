namespace IdolVillage.Scenes.World1.Data
{
    internal class TechTree
    {
        internal static TechTree Instance = new();

        internal List<Technology> Technologies = new();

        internal List<Technology> GetAvailableTechnologies()
        {
            return Technologies.Where(x => x.Researched == false
                && x.Prerequisites.All(y => Technologies.First( z=> z.Key == y).Researched)).ToList();
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
                Key = TechnologyKeys.Foraging,
                Description = "Unlocks the ability to search for mushrooms.",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new() { },
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.Gathering },
                Costs = new() { { Resource.Veggies, 5 } }
            });

            Technologies.Add(new Technology
            {
                Name = "Hunting (protein)",
                Key = TechnologyKeys.HuntingFood,
                Description = "Unlocks the ability to hunt for food.",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new List<TechnologyKeys> { TechnologyKeys.Foraging },
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.HuntingFood },
                Costs = new() {
                    { Resource.Veggies, 10 }
                }
            });

            Technologies.Add(new Technology
            {
                Name = "Hunting (Pelts)",
                Key = TechnologyKeys.HuntingPelts,
                Description = "Unlocks the ability to hunt for Pelts.",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new List<TechnologyKeys> { TechnologyKeys.HuntingFood },
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.HuntingPelts },
                Costs = new() {
                    { Resource.Veggies, 10 },
                    { Resource.Protein, 10 }
                }
            });

            Technologies.Add(new Technology
            {
                Name = "Tanning",
                Key = TechnologyKeys.Tanning,
                Description = "Tannery",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new List<TechnologyKeys> { TechnologyKeys.HuntingPelts },
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.Leather },
                Costs = new() { 
                    { Resource.Veggies, 10 },
                    { Resource.Protein, 10 },
                    { Resource.Pelt, 10 },
                }
            });

            Technologies.Add(new Technology
            {
                Name = "Farming",
                Key = TechnologyKeys.Farming,
                Description = "Unlocks the ability to farm crops.",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new List<TechnologyKeys> { TechnologyKeys.Foraging, TechnologyKeys.HuntingFood },
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.Farm },
                Costs = new() {
                    { Resource.Veggies, 10 },
                    { Resource.Protein, 10 },
                }
            });

            Technologies.Add(new Technology
            {
                Name = "Milling",
                Key = TechnologyKeys.Windmill,
                Description = "Unlocks the ability to farm crops.",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new List<TechnologyKeys> { TechnologyKeys.Farming },
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.Flour },
                Costs = new() {
                    { Resource.Veggies, 10 },
                    { Resource.Protein, 10 },
                    { Resource.Grain, 10 },
                }
            });

            Technologies.Add(new Technology
            {
                Name = "Forestry",
                Key = TechnologyKeys.Forestry,
                Description = "Unlocks the ability to gain wood.",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new List<TechnologyKeys> { TechnologyKeys.HuntingFood },
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.Wood },
                Costs = new() {
                    { Resource.Veggies, 10 },
                    { Resource.Protein, 10 }
                }
            });

            Technologies.Add(new Technology
            {
                Name = "Lumbermill",
                Key = TechnologyKeys.Lumbermill,
                Description = "Unlocks the Lumber mill.",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new List<TechnologyKeys> { TechnologyKeys.Forestry },
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.Lumber },
                Costs = new() {
                    { Resource.Veggies, 10 },
                    { Resource.Protein, 10 },
                    { Resource.Wood, 10 }
                }
            });

            Technologies.Add(new Technology
            {
                Name = "Carpenter",
                Key = TechnologyKeys.Carpenter,
                Description = "Unlocks the Lumber mill.",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new List<TechnologyKeys> { TechnologyKeys.Lumbermill },
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.BuildingMaterials },
                Costs = new() {
                    { Resource.Veggies, 10 },
                    { Resource.Protein, 10 },
                    { Resource.Wood, 10 },
                    { Resource.Lumber, 10 },
                }
            });

            Technologies.Add(new Technology
            {
                Name = "Kitchen",
                Key = TechnologyKeys.Kitchen,
                Description = "Unlocks the Lumber mill.",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new List<TechnologyKeys> { TechnologyKeys.Carpenter },
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.Meals },
                Costs = new() {
                    { Resource.Veggies, 10 },
                    { Resource.Protein, 10 },
                    { Resource.Wood, 10 },
                    { Resource.Lumber, 10 },
                    { Resource.BuildingMaterial, 10 },
                }
            });
            Technologies.Add(new Technology
            {
                Name = "Ranch",
                Key = TechnologyKeys.Ranch,
                Description = "Unlocks the Lumber mill.",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new List<TechnologyKeys> { TechnologyKeys.Carpenter },
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.Wool },
                Costs = new() {
                    { Resource.Veggies, 10 },
                    { Resource.Protein, 10 },
                    { Resource.Wood, 10 },
                    { Resource.Lumber, 10 },
                    { Resource.BuildingMaterial, 10 },
                }
            });

            Technologies.Add(new Technology
            {
                Name = "Weaver",
                Key = TechnologyKeys.Weaver,
                Description = "Unlocks the Lumber mill.",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new List<TechnologyKeys> { TechnologyKeys.Carpenter, TechnologyKeys.Ranch },
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.Cloth },
                Costs = new() {
                    { Resource.Veggies, 10 },
                    { Resource.Protein, 10 },
                    { Resource.Wood, 10 },
                    { Resource.Lumber, 10 },
                    { Resource.BuildingMaterial, 20 },
                }
            });

            Technologies.Add(new Technology
            {
                Name = "Workshop",
                Key = TechnologyKeys.Workshop,
                Description = "Unlocks the Lumber mill.",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new List<TechnologyKeys> { TechnologyKeys.Carpenter },
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.Tools },
                Costs = new() {
                    { Resource.Veggies, 10 },
                    { Resource.Protein, 10 },
                    { Resource.Wood, 10 },
                    { Resource.Lumber, 10 },
                    { Resource.BuildingMaterial, 20 },
                }
            });

            Technologies.Add(new Technology
            {
                Name = "Mines (Gold)",
                Key = TechnologyKeys.MineGold,
                Description = "Unlocks the Lumber mill.",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new List<TechnologyKeys> { TechnologyKeys.Workshop },
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.Gold },
                Costs = new() {
                    { Resource.Veggies, 10 },
                    { Resource.Protein, 10 },
                    { Resource.Wood, 10 },
                    { Resource.Lumber, 10 },
                    { Resource.BuildingMaterial, 20 },
                    { Resource.Tools, 20 },
                }
            });


            Technologies.Add(new Technology
            {
                Name = "Mint",
                Key = TechnologyKeys.Mint,
                Description = "Unlocks the Lumber mill.",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new List<TechnologyKeys> { TechnologyKeys.MineGold },
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.Coins },
                Costs = new() {
                    { Resource.Veggies, 10 },
                    { Resource.Protein, 10 },
                    { Resource.Wood, 10 },
                    { Resource.Lumber, 10 },
                    { Resource.BuildingMaterial, 20 },
                    { Resource.Tools, 20 },
                    { Resource.GoldOre, 20 },
                }
            });

            Technologies.Add(new Technology
            {
                Name = "Mines (Metal)",
                Key = TechnologyKeys.MineMetal,
                Description = "Unlocks the Lumber mill.",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new List<TechnologyKeys> { TechnologyKeys.Workshop },
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.MetalOre },
                Costs = new() {
                    { Resource.Veggies, 10 },
                    { Resource.Protein, 10 },
                    { Resource.Wood, 10 },
                    { Resource.Lumber, 10 },
                    { Resource.BuildingMaterial, 20 },
                    { Resource.Tools, 20 },
                }
            });

            Technologies.Add(new Technology
            {
                Name = "Smithy",
                Key = TechnologyKeys.Smithy,
                Description = "Unlocks the Lumber mill.",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new List<TechnologyKeys> { TechnologyKeys.MineMetal},
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.Metal },
                Costs = new() {
                    { Resource.Veggies, 10 },
                    { Resource.Protein, 10 },
                    { Resource.Wood, 10 },
                    { Resource.Lumber, 10 },
                    { Resource.BuildingMaterial, 20 },
                    { Resource.Tools, 20 },
                    { Resource.MetalOre, 20 },
                }
            });

            Technologies.Add(new Technology
            {
                Name = "Quarry",
                Key = TechnologyKeys.Quarry,
                Description = "Unlocks the Lumber mill.",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new List<TechnologyKeys> { TechnologyKeys.Workshop },
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.Stone },
                Costs = new() {
                    { Resource.Veggies, 10 },
                    { Resource.Protein, 10 },
                    { Resource.Wood, 10 },
                    { Resource.Lumber, 10 },
                    { Resource.BuildingMaterial, 20 },
                    { Resource.Tools, 20 },
                }
            });

            Technologies.Add(new Technology
            {
                Name = "Mason",
                Key = TechnologyKeys.Mason,
                Description = "Unlocks the Lumber mill.",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new List<TechnologyKeys> { TechnologyKeys.Quarry },
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.StoneBuildingMaterials },
                Costs = new() {
                    { Resource.Veggies, 10 },
                    { Resource.Protein, 10 },
                    { Resource.Wood, 10 },
                    { Resource.Lumber, 10 },
                    { Resource.BuildingMaterial, 20 },
                    { Resource.Tools, 20 },
                    { Resource.Stone, 20 },
                }
            });

        }
    }

    internal class Technology
    {
        internal string Name = "";
        internal string Description = "";
        internal List<TechnologyKeys> Prerequisites = new();
        internal bool Researched = false;
        internal float ResearchCost = 0f;
        internal float ResearchTime = 0f;
        internal float ResearchProgress = 0f;
        internal List<ProducerTypes> ProductionToAdd = new();
        internal Dictionary<Resource, double> Costs = new();
        internal TechnologyKeys Key;
    }

    internal enum TechnologyKeys
    {
        Foraging,
        HuntingFood,
        Farming,
        Forestry,
        Lumbermill,
        HuntingPelts,
        Tanning,
        Windmill,
        Carpenter,
        Kitchen,
        Ranch,
        Weaver,
        Workshop,
        MineGold,
        MineMetal,
        Quarry,
        Mason,
        Smithy,
        Mint
    }

    // - gathering - 30% consistent, 5% chance to return toxic berries/flora
    // - villagers impacted by toxic berries may not perform their tasks for 24 hours
    // - foraging - 40% consistent, 10% chance to return toxic mushrooms/fungus
    // - villagers impacted by toxic mushrooms/fungus may revolt against your diety(reducing overall faith) for 24 hours.
    // - hunting - 10% consistent, but on successful hunt, returned food is multiplied by 2-6.  5% chance that the hunter dies on the hunt and does not return.
    // - trapping - 15% consistent, the first time a trapper goes out, it takes full time and returns with no food.Every time after that, takes half the normal time.
    // - fishing - 20% consistent.  .5 modifier on yield due to waste.  Fishing waste directly contributes benefically to agriculture (when implemented)
}
