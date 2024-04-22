using Arch.Core;
using Arch.Core.Extensions;
using IdolVillage;
using IdolVillage.Extensions;
using IdolVillage.Scenes.Components;
using System.Numerics;

namespace IdolVillage.Scenes.World1.Systems
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
                    if (distance > 5)
                    {
                        direction = Vector2.Normalize(direction);
                        render.Position += direction * 5;
                        unit.CurrentAction = Unit.UnitActions.Move;
                    }
                    else
                    {
                        if (unit.AssignedTo != default)
                        {
                            var assignedToRender = unit.AssignedTo.Entity.Get<Render>();

                            if (assignedToRender.Position.DistanceTo(render.Position) < 10)
                            {
                                unit.CurrentAction = Unit.UnitActions.Work;
                            }
                        }
                        unit.MovementGoal = Vector2.Zero;
                    }
                }
            });
        }
    }
}
