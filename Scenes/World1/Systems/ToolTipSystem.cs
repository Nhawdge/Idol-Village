using Arch.Core;
using IdolVillage;
using IdolVillage.Scenes.World1.Helpers;
using IdolVillage.Utilities;

namespace IdolVillage.Scenes.World1.Systems
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
