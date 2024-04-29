using IdolVillage.Utilities;

namespace IdolVillage.Scenes.World1.Data
{
    internal class TechTree
    {
        internal static TechTree Instance = new();

        internal List<Technology> Technologies = new();

        internal List<Technology> GetAvailableTechnologies()
        {
            return Technologies.Where(x => x.Researched == false
                && x.Prerequisites.All(y => Technologies.First(z => z.Key == y).Researched)).ToList();
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
                ToolTipDescription = "Unlocks the ability to search for mushrooms",
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
                ToolTipDescription = "Unlocks hunting for food",
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
                ToolTipDescription = "Unlocks hunting for pelts",
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
                ToolTipDescription = "Unlocks the ability to tan pelts into leather.",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new List<TechnologyKeys> { TechnologyKeys.HuntingPelts },
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.Leather, ProducerTypes.WorshipRug },
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
                ToolTipDescription = "Unlocks the ability to farm grains",
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
                ToolTipDescription = "Unlocks the ability to mill grains into flour",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new List<TechnologyKeys> { TechnologyKeys.Farming, TechnologyKeys.Weaver },
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.Windmill },
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
                ToolTipDescription = "Unlocks the ability to harvest wood",
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
                ToolTipDescription = "Unlocks the ability to mill wood into lumber",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new List<TechnologyKeys> { TechnologyKeys.Forestry },
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.Sawmill },
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
                ToolTipDescription = "Unlocks the ability to build a carpenter's workshop to produce Building Materials.",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new List<TechnologyKeys> { TechnologyKeys.Lumbermill },
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.Carpenter, ProducerTypes.Altar },
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
                ToolTipDescription = "Unlocks the ability to build a kitchen for making Meals.",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new List<TechnologyKeys> { TechnologyKeys.Carpenter },
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.Kitchen },
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
                ToolTipDescription = "Unlocks the ability to build a ranch for producing Wool.",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new List<TechnologyKeys> { TechnologyKeys.Carpenter },
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.RanchWool },
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
                ToolTipDescription = "Unlocks the ability to build a weaver's workshop for producing Cloth.",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new List<TechnologyKeys> { TechnologyKeys.Carpenter, TechnologyKeys.Ranch },
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.Weaver },
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
                ToolTipDescription = "Unlocks the ability to build a workshop for producing Tools.",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new List<TechnologyKeys> { TechnologyKeys.Carpenter },
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.Workshop },
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
                ToolTipDescription = "Unlocks the ability to build a mine for mining Gold Ore.",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new List<TechnologyKeys> { TechnologyKeys.Workshop },
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.GoldMine },
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
                ToolTipDescription = "Unlocks the ability to build a mint for producing Coins.",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new List<TechnologyKeys> { TechnologyKeys.MineGold },
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.Mint },
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
                ToolTipDescription = "Unlocks the ability to build a mine for mining Metal Ore.",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new List<TechnologyKeys> { TechnologyKeys.Workshop },
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.MetalMine },
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
                ToolTipDescription = "Unlocks the ability to build a smithy for producing Metal bars",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new List<TechnologyKeys> { TechnologyKeys.MineMetal },
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.Smithy },
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
                ToolTipDescription = "Unlocks the ability to build a quarry for mining Stone.",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new List<TechnologyKeys> { TechnologyKeys.Workshop },
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.Quarry },
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
                ToolTipDescription = "Unlocks the ability to build a mason's workshop for producing Stone Building Materials.",
                ResearchCost = 10f,
                ResearchTime = 10f,
                Prerequisites = new List<TechnologyKeys> { TechnologyKeys.Quarry },
                Researched = false,
                ProductionToAdd = new() { ProducerTypes.StoneMason, ProducerTypes.Temple },
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
        internal string Name = string.Empty;
        internal string Description = string.Empty;
        private string _ToolTipDescription;
        internal string ToolTipDescription
        {
            get => _ToolTipDescription;
            set => _ToolTipDescription = StringUtilities.GetDescriptionForTooltip(value);
        }

        internal List<TechnologyKeys> Prerequisites = new();
        internal bool Researched = false;
        internal float ResearchCost = 0f;
        internal float ResearchTime = 0f;
        internal float ResearchProgress = 0f;
        internal List<ProducerTypes> ProductionToAdd = new();
        internal Dictionary<Resource, double> Costs = new();
        internal TechnologyKeys Key;

        //public Technology()
        //{
        //Description = description;
        //ToolTipDescription = StringUtilities.GetDescriptionForTooltip(description);
        //}


        internal void CompleteResearch()
        {
            Researched = true;
            foreach (var producerKey in ProductionToAdd)
            {
                ProducerStore.Instance.Producers[producerKey].IsAvailable = true;
            }
        }
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
