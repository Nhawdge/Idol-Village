using Arch.Core.Utils;
using Raylib_cs;
using VillageIdle.Scenes.Components;
using VillageIdle.Utilities;

namespace VillageIdle.Scenes.World1.Data
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

        private void LoadProductionData()
        {
            Producers.Add(ProducerTypes.Gathering, new Producer
            {
                Key = ProducerTypes.Gathering,
                Name = "Gathering",
                SpriteRect = SpriteSheetStore.Instance.GetDecorSheetSource(SpriteKey.Bushes),
                ProductionRequired = 10f,
                ProducedPerSecond = 1f,
                ResourceAmountProduced = 1f,
                Resource = Resource.Veggies,
                ChanceToSucceed = 1f,
                FailureAction = () => { },
            });
            Producers.Add(ProducerTypes.Hunting, new Producer
            {
                Key = ProducerTypes.Hunting,
                Name = "Hunting",
                SpriteRect = SpriteSheetStore.Instance.GetDecorSheetSource(SpriteKey.Bushes),
                ProductionRequired = 5f,
                ProducedPerSecond = 1f,
                ResourceAmountProduced = 1f,
                Resource = Resource.Protien,
                ChanceToSucceed = 1f,
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
                Cost = new Dictionary<Resource, int> { { Resource.Wood, 1 } },
                FailureAction = () => { }
            });
        }
    }

    public class Producer
    {
        public string Name = "None";
        public float ProductionRequired = 1f;
        public float ProducedPerSecond = 1f;
        public float ResourceAmountProduced = 1f;
        public Resource Resource = Resource.None;
        public float ChanceToSucceed = 1f;
        public Action FailureAction = () => { };
        public bool IsAvailable = false;
        internal ProducerTypes Key;
        internal Dictionary<Resource, int> Cost;
        internal Rectangle SpriteRect;
    }
    public enum ProducerTypes
    {
        None,
        Gathering,
        Hunting,
        Farm,
        Wood,
        Lumber,
    }
}
