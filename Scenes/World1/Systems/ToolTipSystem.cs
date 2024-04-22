using Arch.Core;
using VillageIdle.Scenes.World1.Helpers;
using VillageIdle.Utilities;

namespace VillageIdle.Scenes.World1.Systems
{
    internal class ToolTipSystem : GameSystem
    {
        internal override void Update(World world) { }

        internal override void UpdateNoCamera(World world)
        {
            UiHelpers.DrawToolTipOnMouse();
            InteractionHelper.ClickProcessed = false;
        }
    }
}
