using Arch.Core;
using IdolVillage.Scenes.World1.Data;
using IdolVillage.Utilities;
using Raylib_cs;

namespace IdolVillage.Scenes.World1.Systems
{
    internal class BeliefSystem : GameSystem
    {
        internal float time = 0;
        internal override void Update(World world)
        {
            time += Raylib.GetFrameTime();
            if (time > 1)
            {
                time -= 1;
                var belief = VillageData.Instance.Resources[Resource.Belief];
                var toRemove = EasingHelpers.easeInQuad(belief / 10);
                Console.WriteLine($"Removing {toRemove} belief");
                VillageData.Instance.Resources[Resource.Belief] = Math.Max(VillageData.Instance.Resources[Resource.Belief] - toRemove, 0);
            }
        }
    }
}
