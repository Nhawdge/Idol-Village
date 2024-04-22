﻿using Arch.Core;
using System.Numerics;
using VillageIdle.Scenes.Components;
using VillageIdle.Scenes.World1.Data;
using VillageIdle.Utilities;

namespace VillageIdle.Scenes.World1.Systems
{
    internal class RecruitmentSystem : GameSystem
    {
        internal override void Update(World world)
        {
            var popCap = VillageData.Instance.TotalPrestige / 10;
            if (popCap > VillageData.Instance.TotalPopulation)
            {
                var render = new Render(TextureKey.MedievalSpriteSheet);
                render.SetSource(SpriteSheetStore.Instance.GetUnitSheetSource(SpriteKey.Woman));
                render.Position = new Vector2(50 * 128, 50 * 128);
                var unit = new Unit
                {
                    MovementGoal = new Vector2(50 * 128, 50 * 128)
                };
                world.Create(render, unit, new Interactable(), new UnitLayer());
                VillageData.Instance.TotalPopulation++;
                Console.WriteLine("Unit Spawned");
            }

        }
    }
}
