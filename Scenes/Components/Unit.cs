using System.Numerics;

namespace VillageIdle.Scenes.Components
{
    internal class Unit
    {
        public UnitActions CurrentAction = UnitActions.Idle;
        public Vector2 MovementGoal = new Vector2(0, 0);
        public int AssignedTo = -1;
        public HealthStatus Health = HealthStatus.Healthy;
        public int HealthValue = 100;
        public int HealthStatusDuration = 0;

        public enum UnitActions
        {
            Idle,
            Move,
            Work,
            Gather,
            Build
        }

        public enum HealthStatus
        {
            Healthy,
            Sick, // damage over time to health
            Injured, // spike damage to health
            Dead
        }
    }
}
