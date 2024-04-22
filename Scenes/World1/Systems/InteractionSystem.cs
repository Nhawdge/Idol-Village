﻿using Arch.Core;
using Arch.Core.Extensions;
using IdolVillage;
using IdolVillage.Scenes.Components;
using IdolVillage.Scenes.World1.Data;
using IdolVillage.Utilities;
using Raylib_cs;

namespace IdolVillage.Scenes.World1.Systems
{
    internal class InteractionSystem : GameSystem
    {
        internal override void Update(World world)
        {
            var query = new QueryDescription().WithAll<Interactable, Render>();
            var mousePos = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), IdolVillageEngine.Instance.Camera);
            world.Query(in query, (entity) =>
            {
                var interactable = entity.Get<Interactable>();
                var render = entity.Get<Render>();
                if (Raylib.CheckCollisionPointRec(mousePos, render.CollisionDestination))
                {
                    render.Color = Color.Gray;
                    if (InteractionHelper.GetMouseClick(MouseButton.Left))
                    {
                        if (Singleton.Instance.SelectedUnit == entity.Id)
                            Singleton.Instance.SelectedUnit = -1;
                        else
                            Singleton.Instance.SelectedUnit = entity.Id;
                    }
                }
                else
                {
                    render.Color = Color.White;
                }

            });
        }
    }
}
