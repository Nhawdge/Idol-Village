using Arch.Core;
using VillageIdle.Scenes.Components;
using VillageIdle.Scenes.World1.Components;
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
                var render = new Render(Utilities.TextureKey.MedievalSpriteSheet);
                render.SetSource(SpriteSheetStore.Instance.GetUnitSheetSource(SpriteKey.Woman));
                var unit = new Unit
                {
                    MovementGoal = new System.Numerics.Vector2(50 * 128, 50 * 128)
                };
                world.Create(render, unit, new UnitLayer());
                VillageData.Instance.TotalPopulation++;
                Console.WriteLine("Unit Spawned");
            }

        }
    }
}
