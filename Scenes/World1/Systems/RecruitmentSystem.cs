using Arch.Core;
using IdolVillage.Scenes.Components;
using IdolVillage.Scenes.World1.Data;
using IdolVillage.Utilities;
using System.Numerics;

namespace IdolVillage.Scenes.World1.Systems
{
    internal class RecruitmentSystem : GameSystem
    {
        internal override void Update(World world)
        {
            var popCap = VillageData.Instance.TotalPrestige / 10;
            if (popCap > VillageData.Instance.TotalPopulation)
            {
                var render = new Sprite(TextureKey.Units, "Assets/Art/Units");
                render.SpriteWidth = 64;
                render.SpriteHeight = 64;

                render.Position = new Vector2(50 * 128, 50 * 128);
                render.Play("villager2-idle");
                var unit = new Unit
                {
                    MovementGoal = new Vector2(50 * 128, 50 * 128)
                };
                world.Create(render, unit, new Interactable() {  Name = "Villager" }, new UnitLayer());
                VillageData.Instance.TotalPopulation++;
            }

        }
    }
}
