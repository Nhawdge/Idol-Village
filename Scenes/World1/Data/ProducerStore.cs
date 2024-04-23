using Arch.Core;
using Arch.Core.Extensions;
using Arch.Core.Utils;
using IdolVillage.Scenes.Components;
using IdolVillage.Utilities;
using Raylib_cs;
using System.Numerics;

namespace IdolVillage.Scenes.World1.Data
{
    internal class ProducerStore
    {
        internal static ProducerStore Instance = new();
        internal Dictionary<ProducerTypes, Producer> Producers = new();

        internal static readonly ComponentType[] Producer = [typeof(ProductionUnit), typeof(Render), typeof(StructureLayer), typeof(Interactable)];
        internal List<Producer> GetAvailableProducers() => Producers.Values.Where(x => x.IsAvailable).ToList();

        private ProducerStore()
        {
            LoadProductionData();
        }

        internal bool IsProducerBuildable(ProducerTypes producer)
        {
            var costs = Producers[producer]?.BuildCost;
            if (costs == null) return true;

            var canAfford = true;
            foreach (var cost in costs)
            {
                if (VillageData.Instance.Resources[cost.Key] < cost.Value)
                {
                    canAfford = false;
                    break;
                }
            }
            return canAfford;
        }
        internal bool CanProducerProduce(ProducerTypes producer)
        {
            var costs = Producers[producer].ProductionCost;

            var canAfford = true;
            foreach (var cost in costs)
            {
                if (VillageData.Instance.Resources[cost.Key] < cost.Value)
                {
                    canAfford = false;
                    break;
                }
            }
            return canAfford;
        }

        internal static void CreateProducer(World world, Producer production)
        {
            var structureQuery = new QueryDescription().WithAll<StructureLayer>();
            var totalStructures = world.CountEntities(structureQuery);

            var producer = world.Create(Producer);
            producer.Set(new StructureLayer());
            producer.Set(new Interactable() { Name = production.Name });
            producer.Set(new ProductionUnit { Producer = production.Key });
            var render = new Render(TextureKey.MedievalSpriteSheet);
            render.SetSource(production.SpriteRect);

            var gridSize = 128;

            var worldCenter = new Vector2(50, 50);



            var gridCoords = worldCenter + GridUtilities.GetNextPosition(totalStructures);
            render.Position = gridCoords * gridSize;
            producer.Set(render);

            VillageData.Instance.ApplyCosts(production.BuildCost);
        }


        private void LoadProductionData()
        {
            Producers.Add(ProducerTypes.Gathering, new Producer
            {
                Key = ProducerTypes.Gathering,
                Name = "Bushes",
                SpriteRect = SpriteSheetStore.Instance.GetDecorSheetSource(SpriteKey.Bushes),
                ProductionRequired = 10f,
                ProducedPerSecond = 1f,
                ResourceAmountProduced = 1f,
                Resource = Resource.Veggies,
                ChanceToSucceed = 1f,
                WorkerCapacity = 3,
                BuildCost = new Dictionary<Resource, double> { { Resource.Veggies, 5 } },
                FailureAction = () => { },
            });

            Producers.Add(ProducerTypes.HuntingFood, new Producer
            {
                Key = ProducerTypes.HuntingFood,
                Name = "Hunting (Food)",
                SpriteRect = SpriteSheetStore.Instance.GetDecorSheetSource(SpriteKey.Bushes),
                ProductionRequired = 5f,
                ProducedPerSecond = 1f,
                ResourceAmountProduced = 1f,
                Resource = Resource.Protein,
                ChanceToSucceed = 1f,
                BuildCost = new Dictionary<Resource, double> { { Resource.Veggies, 10 } },
                FailureAction = () => { }
            });

            Producers.Add(ProducerTypes.HuntingPelts, new Producer
            {
                Key = ProducerTypes.HuntingPelts,
                Name = "Hunting (Pelts)",
                SpriteRect = SpriteSheetStore.Instance.GetDecorSheetSource(SpriteKey.Bushes),
                ProductionRequired = 5f,
                ProducedPerSecond = 1f,
                ResourceAmountProduced = 1f,
                Resource = Resource.Pelt,
                ChanceToSucceed = 1f,
                BuildCost = new Dictionary<Resource, double> { { Resource.Veggies, 10 } },
                FailureAction = () => { }
            });

            Producers.Add(ProducerTypes.Farm, new Producer
            {
                Key = ProducerTypes.Farm,
                Name = "Farming",
                SpriteRect = SpriteSheetStore.Instance.GetTileSheetSource(SpriteKey.BigFarm),
                ProductionRequired = 10f,
                ProducedPerSecond = 1f,
                ResourceAmountProduced = 10f,
                Resource = Resource.Grain,
                ChanceToSucceed = 1f,
                BuildCost = new Dictionary<Resource, double> { },
                FailureAction = () => { }
            });
            Producers.Add(ProducerTypes.Wood, new Producer
            {
                Key = ProducerTypes.Wood,
                Name = "Forestry",
                SpriteRect = SpriteSheetStore.Instance.GetDecorSheetSource(SpriteKey.Tree),
                ProductionRequired = 10f,
                ProducedPerSecond = 1f,
                ResourceAmountProduced = 1f,
                Resource = Resource.Wood,
                ChanceToSucceed = 1f,
                BuildCost = new Dictionary<Resource, double> { },
                FailureAction = () => { }
            });

            Producers.Add(ProducerTypes.Lumber, new Producer
            {
                Key = ProducerTypes.Lumber,
                Name = "Lumber",
                SpriteRect = SpriteSheetStore.Instance.GetStructureSheetSource(SpriteKey.Tent),
                ProductionRequired = 10f,
                ProducedPerSecond = 1f,
                ResourceAmountProduced = 1f,
                Resource = Resource.Lumber,
                ChanceToSucceed = 1f,
                BuildCost = new Dictionary<Resource, double> { },
                ProductionCost = new Dictionary<Resource, double> { { Resource.Wood, 1 } },
                FailureAction = () => { }
            });

            Producers.Add(ProducerTypes.Leather, new Producer
            {
                Key = ProducerTypes.Leather,
                Name = "Leatherworker",
                SpriteRect = SpriteSheetStore.Instance.GetStructureSheetSource(SpriteKey.Tent),
                ProductionRequired = 10f,
                ProducedPerSecond = 1f,
                ResourceAmountProduced = 1f,
                Resource = Resource.Leather,
                ChanceToSucceed = 1f,
                BuildCost = new Dictionary<Resource, double> { },
                ProductionCost = new Dictionary<Resource, double> { { Resource.Pelt, 1 } },
                FailureAction = () => { }
            });

            Producers.Add(ProducerTypes.Flour, new Producer
            {
                Key = ProducerTypes.Flour,
                Name = "Windmill",
                SpriteRect = SpriteSheetStore.Instance.GetStructureSheetSource(SpriteKey.Tent),
                ProductionRequired = 10f,
                ProducedPerSecond = 1f,
                ResourceAmountProduced = 1f,
                Resource = Resource.Flour,
                ChanceToSucceed = 1f,
                BuildCost = new Dictionary<Resource, double> { },
                ProductionCost = new Dictionary<Resource, double> { { Resource.Grain, 5 } },
                FailureAction = () => { }
            });

            Producers.Add(ProducerTypes.BuildingMaterials, new Producer
            {
                Key = ProducerTypes.BuildingMaterials,
                Name = "Carpenter",
                SpriteRect = SpriteSheetStore.Instance.GetStructureSheetSource(SpriteKey.Tent),
                ProductionRequired = 10f,
                ProducedPerSecond = 1f,
                ResourceAmountProduced = 1f,
                Resource = Resource.BuildingMaterial,
                ChanceToSucceed = 1f,
                BuildCost = new Dictionary<Resource, double> { },
                ProductionCost = new Dictionary<Resource, double> { { Resource.Lumber, 10 } },
                FailureAction = () => { }
            });
            Producers.Add(ProducerTypes.Meals, new Producer
            {
                Key = ProducerTypes.Meals,
                Name = "Kitchen",
                SpriteRect = SpriteSheetStore.Instance.GetStructureSheetSource(SpriteKey.Tent),
                ProductionRequired = 10f,
                ProducedPerSecond = 10f,
                ResourceAmountProduced = 1f,
                Resource = Resource.BuildingMaterial,
                ChanceToSucceed = 1f,
                BuildCost = new Dictionary<Resource, double> { },
                ProductionCost = new Dictionary<Resource, double> {
                    { Resource.Veggies, 10 },
                    { Resource.Protein, 10 },
                    { Resource.Grain, 10 },
                },
                FailureAction = () => { }
            });

            Producers.Add(ProducerTypes.Wool, new Producer
            {
                Key = ProducerTypes.Wool,
                Name = "Ranch (Sheep)",
                SpriteRect = SpriteSheetStore.Instance.GetStructureSheetSource(SpriteKey.Tent),
                ProductionRequired = 10f,
                ProducedPerSecond = 1,
                ResourceAmountProduced = 1f,
                Resource = Resource.Wool,
                ChanceToSucceed = 1f,
                BuildCost = new Dictionary<Resource, double> { },
                ProductionCost = new Dictionary<Resource, double> {
                    { Resource.Veggies, 10 },
                },
                FailureAction = () => { }
            });

            Producers.Add(ProducerTypes.Cloth, new Producer
            {
                Key = ProducerTypes.Cloth,
                Name = "Weaver",
                SpriteRect = SpriteSheetStore.Instance.GetStructureSheetSource(SpriteKey.Tent),
                ProductionRequired = 10f,
                ProducedPerSecond = 1,
                ResourceAmountProduced = 1f,
                Resource = Resource.Cloth,
                ChanceToSucceed = 1f,
                BuildCost = new Dictionary<Resource, double> { },
                ProductionCost = new Dictionary<Resource, double> {
                    { Resource.Wool, 1 },
                },
                FailureAction = () => { }
            });

            Producers.Add(ProducerTypes.Tools, new Producer
            {
                Key = ProducerTypes.Tools,
                Name = "Workshop",
                SpriteRect = SpriteSheetStore.Instance.GetStructureSheetSource(SpriteKey.Tent),
                ProductionRequired = 10f,
                ProducedPerSecond = 1,
                ResourceAmountProduced = 1f,
                Resource = Resource.Tools,
                ChanceToSucceed = 1f,
                BuildCost = new Dictionary<Resource, double> { },
                ProductionCost = new Dictionary<Resource, double> {
                    { Resource.BuildingMaterial, 10 },
                },
                FailureAction = () => { }
            });

            Producers.Add(ProducerTypes.Gold, new Producer
            {
                Key = ProducerTypes.Gold,
                Name = "Gold Mine",
                SpriteRect = SpriteSheetStore.Instance.GetStructureSheetSource(SpriteKey.Tent),
                ProductionRequired = 10f,
                ProducedPerSecond = 1,
                ResourceAmountProduced = 1f,
                Resource = Resource.GoldOre,
                ChanceToSucceed = 1f,
                BuildCost = new Dictionary<Resource, double> { },
                ProductionCost = new Dictionary<Resource, double> {
                    { Resource.Tools, 1 },
                },
                FailureAction = () => { }
            });

            Producers.Add(ProducerTypes.Coins, new Producer
            {
                Key = ProducerTypes.Coins,
                Name = "Mint",
                SpriteRect = SpriteSheetStore.Instance.GetStructureSheetSource(SpriteKey.Tent),
                ProductionRequired = 30f,
                ProducedPerSecond = 1,
                ResourceAmountProduced = 100f,
                Resource = Resource.Coins,
                ChanceToSucceed = 1f,
                BuildCost = new Dictionary<Resource, double> { },
                ProductionCost = new Dictionary<Resource, double> {
                    { Resource.GoldOre, 10 },
                },
                FailureAction = () => { }
            });

            Producers.Add(ProducerTypes.MetalOre, new Producer
            {
                Key = ProducerTypes.MetalOre,
                Name = "Metal Mine",
                SpriteRect = SpriteSheetStore.Instance.GetStructureSheetSource(SpriteKey.Tent),
                ProductionRequired = 10f,
                ProducedPerSecond = 1,
                ResourceAmountProduced = 1f,
                Resource = Resource.MetalOre,
                ChanceToSucceed = 1f,
                BuildCost = new Dictionary<Resource, double> { },
                ProductionCost = new Dictionary<Resource, double> {
                    { Resource.Tools, 1 },
                },
                FailureAction = () => { }
            });


            Producers.Add(ProducerTypes.Metal, new Producer
            {
                Key = ProducerTypes.Metal,
                Name = "Smithy",
                SpriteRect = SpriteSheetStore.Instance.GetStructureSheetSource(SpriteKey.Tent),
                ProductionRequired = 10f,
                ProducedPerSecond = 1,
                ResourceAmountProduced = 1f,
                Resource = Resource.Metal,
                ChanceToSucceed = 1f,
                BuildCost = new Dictionary<Resource, double> { },
                ProductionCost = new Dictionary<Resource, double> {
                    { Resource.MetalOre, 1 },
                },
                FailureAction = () => { }
            });

            Producers.Add(ProducerTypes.Stone, new Producer
            {
                Key = ProducerTypes.Stone,
                Name = "Quarry",
                SpriteRect = SpriteSheetStore.Instance.GetStructureSheetSource(SpriteKey.Tent),
                ProductionRequired = 10f,
                ProducedPerSecond = 1,
                ResourceAmountProduced = 1f,
                Resource = Resource.Stone,
                ChanceToSucceed = 1f,
                BuildCost = new Dictionary<Resource, double> { },
                ProductionCost = new Dictionary<Resource, double> {
                    { Resource.Tools, 1 },
                },
                FailureAction = () => { }
            });

            Producers.Add(ProducerTypes.StoneBuildingMaterials, new Producer
            {
                Key = ProducerTypes.StoneBuildingMaterials,
                Name = "Mason",
                SpriteRect = SpriteSheetStore.Instance.GetStructureSheetSource(SpriteKey.Tent),
                ProductionRequired = 10f,
                ProducedPerSecond = 1,
                ResourceAmountProduced = 1f,
                Resource = Resource.StoneBuildingMaterials,
                ChanceToSucceed = 1f,
                BuildCost = new Dictionary<Resource, double> { },
                ProductionCost = new Dictionary<Resource, double> {
                    { Resource.Tools, 1 },
                    { Resource.Stone, 2 },
                },
                FailureAction = () => { }
            });

            Producers.Add(ProducerTypes.None, new Producer
            {
                Key = ProducerTypes.None,
                Name = "Test - None",
                SpriteRect = SpriteSheetStore.Instance.GetStructureSheetSource(SpriteKey.Tent),
                ProductionRequired = 10f,
                ProducedPerSecond = 1,
                ResourceAmountProduced = 1f,
                Resource = Resource.None,
                IsAvailable = true,
                ChanceToSucceed = 1f,
                BuildCost = new Dictionary<Resource, double> { },
                ProductionCost = new Dictionary<Resource, double> {
                },
                FailureAction = () => { }
            });
        }
    }

    public class Producer
    {
        public string Name = "None";
        public Resource Resource = Resource.None;
        public float ProductionRequired = 1f;
        public float ProducedPerSecond = 1f;
        public float ResourceAmountProduced = 1f;
        public float ChanceToSucceed = 1f;
        public bool IsAvailable = false;
        internal ProducerTypes Key;
        internal Rectangle SpriteRect;
        internal Dictionary<Resource, double> ProductionCost = new();
        internal Dictionary<Resource, double> BuildCost = new();
        internal int WorkerCapacity = 3;

        public Action FailureAction = () => { };
    }
    public enum ProducerTypes
    {
        None,
        Gathering,
        HuntingFood,
        Farm,
        Wood,
        Lumber,
        HuntingPelts,
        Leather,
        Flour,
        BuildingMaterials,
        Meals,
        Wool,
        Cloth,
        Tools,
        Gold,
        MetalOre,
        Stone,
        StoneBuildingMaterials,
        Metal,
        Coins,
    }
}
