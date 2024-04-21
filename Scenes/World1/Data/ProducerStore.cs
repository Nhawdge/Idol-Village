using Arch.Core.Utils;
using VillageIdle.Scenes.Components;

namespace VillageIdle.Scenes.World1.Data
{
    internal class ProducerStore
    {
        internal static ProducerStore Instance = new();
        internal Dictionary<ProducerTypes, Producer> Producers = new();

        internal static readonly ComponentType[] Producer = [typeof(ProductionUnit), typeof(Render), typeof(StructureLayer)];
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
                ProductionRequired = 10f,
                ProducedPerSecond = 1f,
                ResourceAmountProduced = 1f,
                Resource = Resource.Food,
                ChanceToSucceed = 1f,
                FailureAction = () => { },
            });
            Producers.Add(ProducerTypes.Hunting, new Producer
            {
                Key = ProducerTypes.Hunting,
                Name = "Hunting",
                ProductionRequired = 5f,
                ProducedPerSecond = 1f,
                ResourceAmountProduced = 1f,
                Resource = Resource.Food,
                ChanceToSucceed = 1f,
                FailureAction = () => { }
            });
            Producers.Add(ProducerTypes.Farm, new Producer
            {
                Key = ProducerTypes.Farm,
                Name = "Farming",
                ProductionRequired = 10f,
                ProducedPerSecond = 1f,
                ResourceAmountProduced = 10f,
                Resource = Resource.Food,
                ChanceToSucceed = 1f,
                FailureAction = () => { }
            });
            Producers.Add(ProducerTypes.Wood, new Producer
            {
                Key = ProducerTypes.Wood,
                Name = "Forestry",
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
                ProductionRequired = 10f,
                ProducedPerSecond = 1f,
                ResourceAmountProduced = 1f,
                Resource = Resource.Lumber,
                ChanceToSucceed = 1f,
                Requires = new Dictionary<ProducerTypes, int> { { ProducerTypes.Wood, 1 } },
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
        internal Dictionary<ProducerTypes, int> Requires;
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
