using IdolVillage.Scenes.World1.Data;

namespace IdolVillage.Scenes.Components
{
    internal class ProductionUnit
    {
        public float CurrentProduction = 0f;
        public bool InProduction = false;
        public ProducerTypes Producer = ProducerTypes.None;
    }
}
