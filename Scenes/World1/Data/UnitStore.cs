using Arch.Core;
using Arch.Core.Utils;
using IdolVillage.Scenes.Components;
using IdolVillage.Utilities;
using Raylib_cs;
using System.Numerics;

namespace IdolVillage.Scenes.World1.Data
{
    internal class UnitStore
    {
        internal static UnitStore Instance = new();
        internal Dictionary<ProducerTypes, Producer> Producers = new();

        internal static readonly ComponentType[] Producer = [typeof(Sprite), typeof(Unit), typeof(Interactable), typeof(UnitLayer)];

        internal static List<UnitRoles> AvailableUnits() => new List<UnitRoles> { UnitRoles.Villager };
        internal Dictionary<UnitRoles, Dictionary<Resource, double>> UnitCosts = new();

        private UnitStore()
        {
            UnitCosts.Add(UnitRoles.Villager, new Dictionary<Resource, double> {
                { Resource.Veggies, 2 },
                { Resource.Protein, 2 },
            });
        }

        internal static void CreateUnit(World world, UnitRoles unitRole)
        {
            if (AvailableUnits().Contains(unitRole))
            {
                switch (unitRole)
                {
                    case UnitRoles.Villager:
                        CreateVillager(world);
                        break;
                    case UnitRoles.Solider:
                        break;
                    case UnitRoles.SoldierShield:
                        break;
                    case UnitRoles.Priest:
                        break;
                    case UnitRoles.HighPriest:
                        break;
                    default:
                        break;
                }
            }
        }

        private static void CreateVillager(World world)
        {
            var render = new Render(TextureKey.MedievalSpriteSheet);
            render.SetSource(SpriteSheetStore.Instance.GetUnitSheetSource(Random.Shared.Next(0, 1) == 0 ? SpriteKey.Woman : SpriteKey.Man));
            render.OriginPos = Render.OriginAlignment.LeftTop;
            render.Position = new Vector2(50 * 128, 50 * 128);
            var unit = new Unit
            {
                MovementGoal = new Vector2(50 * 128, 50 * 128)
            };
            world.Create(render, unit, new Interactable() { Name = "Villager" }, new UnitLayer());
        }

        internal bool IsUnitAvailable(UnitRoles unit)
        {
            var costs = UnitCosts[unit];

            var canAfford = true;
            foreach (var cost in costs)
            {
                if (VillageData.Instance.Resources[cost.Key] < cost.Value)
                {
                    canAfford = false;
                    break;
                }
            }

            return canAfford;
        }

        internal enum UnitRoles
        {
            None,
            Villager,
            Solider,
            SoldierShield,
            Priest,
            HighPriest,
        }
    }

}
