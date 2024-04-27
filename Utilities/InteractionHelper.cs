using Raylib_cs;

namespace IdolVillage.Utilities
{
    internal class InteractionHelper
    {
        public static bool ClickProcessed = false;
        public static bool ScrollProcessed = false;

        internal static bool GetMouseClick(MouseButton button)
        {
            if (!ClickProcessed && Raylib.IsMouseButtonPressed(button))
            {
                ClickProcessed = true;
                return true;
            }

            return false;
        }


        internal static float GetMouseScroll()
        {
            if (!ScrollProcessed && Raylib.GetMouseWheelMove() != 0)
            {
                ScrollProcessed = true;
                return Raylib.GetMouseWheelMove();
            }

            return 0;
        }

    }
}
