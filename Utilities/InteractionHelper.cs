using Raylib_cs;

namespace IdolVillage.Utilities
{
    internal class InteractionHelper
    {
        public static bool ClickProcessed = false;

        internal static bool GetMouseClick(MouseButton button)
        {
            if (!ClickProcessed && Raylib.IsMouseButtonPressed(button))
            {
                ClickProcessed = true;
                return true;
            }

            return false;
        }

    }
}
