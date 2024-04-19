using VillageIdle.Scenes.World1.Data;

namespace VillageIdle.Scenes.Components
{
    internal class ProductionUnit
    {
        public float ProductionTotal = 1f;
        public float ProductionRate = 1f;
        public float ProductionValue = 1f;
        public float CurrentProduction = 0f;
        public Resource Resource = Resource.Food;
        public float ChanceToSucceed = 1f;
        public Action FailureAction = () => { };
    }
}
