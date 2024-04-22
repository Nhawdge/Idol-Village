using VillageIdle.Scenes.World1.Data;

namespace VillageIdle.Scenes.Components
{
    internal class ProductionUnit
    {
        public float CurrentProduction = 0f;
        public bool InProduction = false;
        public ProducerTypes Producer = ProducerTypes.None;
    }
}
