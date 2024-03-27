using Arch.Core;
using Arch.Core.Extensions;
using System.Numerics;
using VillageIdle.Scenes.Components;

namespace VillageIdle.Scenes.World1.Systems
{
    internal class UnitMovementSystem : GameSystem
    {
        internal override void Update(World world)
        {
            var query = new QueryDescription().WithAll<Render, Unit>();
            world.Query(in query, (entity) =>
            {
                var render = entity.Get<Render>();
                var unit = entity.Get<Unit>();
                if (unit.MovementGoal != Vector2.Zero)
                {
                    var direction = unit.MovementGoal - render.Position;
                    var distance = direction.Length();
                    if ( distance> 5)
                    {
                        direction = System.Numerics.Vector2.Normalize(direction);
                        render.Position += direction * 5;
                    }
                    else
                    {
                        unit.MovementGoal = Vector2.Zero;
                    }
                }
            });
        }
    }
}
