﻿using Arch.Core;
using Arch.Core.Extensions;
using IdolVillage;
using IdolVillage.Extensions;
using IdolVillage.Scenes.Components;
using Raylib_cs;
using System.Numerics;

namespace IdolVillage.Scenes.World1.Systems
{
    internal class UnitMovementSystem : GameSystem
    {
        internal override void Update(World world)
        {
            var query = new QueryDescription().WithAll<Sprite, Unit>();
            world.Query(in query, (entity) =>
            {
                var render = entity.Get<Sprite>();
                var unit = entity.Get<Unit>();

                if (unit.CurrentAction == Unit.UnitActions.Move)
                {
                    var direction = unit.MovementGoal - render.Position;
                    var distance = direction.Length();
                    if (distance > 5)
                    {
                        direction = Vector2.Normalize(direction);
                        render.Position += direction * 50 * Raylib.GetFrameTime();
                        unit.CurrentAction = Unit.UnitActions.Move;
                    }
                    else if (unit.AssignedTo != default)
                    {
                        unit.CurrentAction = Unit.UnitActions.Work;
                    }
                    else
                    {
                        unit.CurrentAction = Unit.UnitActions.Idle;
                    }
                }
                else if (unit.CurrentAction == Unit.UnitActions.Work)
                {
                    if (unit.AssignedTo == default || unit.AssignedTo == EntityReference.Null)
                    {
                        unit.CurrentAction = Unit.UnitActions.Idle;
                    }
                    else
                    {
                        var assignedToRender = unit.AssignedTo.Entity.Get<Sprite>();

                        if (unit.MovementGoal.DistanceTo(render.Position) < 5)
                        {
                            var assignedToPosition = unit.AssignedTo.Entity.Get<Sprite>().Position;
                            var randomDirection = new Vector2(Random.Shared.NextSingle() * 2 - 1, Random.Shared.NextSingle() * 2 - 1);
                            unit.MovementGoal = assignedToPosition + randomDirection * 25;
                        }
                        else
                        {
                            var direction = unit.MovementGoal - render.Position;
                            var distance = direction.Length();

                            direction = Vector2.Normalize(direction);
                            render.Position += direction * 10 * Raylib.GetFrameTime();
                        }
                    }
                }
                else if (unit.CurrentAction == Unit.UnitActions.Idle)
                {
                    if (unit.AssignedTo == default || unit.AssignedTo == EntityReference.Null)
                    {
                        var randomDirection = new Vector2(Random.Shared.NextSingle() * 2 - 1, Random.Shared.NextSingle() * 2 - 1);
                        unit.MovementGoal = new Vector2(128 * 50) + randomDirection * 125;
                    }
                    else
                    {
                        unit.CurrentAction = Unit.UnitActions.Move;
                    }
                }
            });
        }
    }
}
