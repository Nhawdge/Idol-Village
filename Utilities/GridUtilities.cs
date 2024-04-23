using System.Drawing;
using System.Numerics;

namespace IdolVillage.Utilities
{
    internal static class GridUtilities
    {
        internal static Vector2 GetNextPosition(int number)
        {
            var nextPosition = new Vector2(0, 0);

            var bounds = new Rectangle(0, 0, 1, 1);
            var direction = Directions.East;
            
            while (number > 0)
            {
                if (direction == Directions.East)
                {
                    if (bounds.Width > nextPosition.X)
                    {
                        nextPosition.X++;
                    }
                    else
                    {
                        bounds.Width++;
                        direction = Directions.South;
                        nextPosition.Y++;
                    }
                }
                else if (direction == Directions.South)
                {
                    if (bounds.Height > nextPosition.Y)
                    {
                        nextPosition.Y++;
                    }
                    else
                    {
                        bounds.Height++;
                        nextPosition.X--;
                        direction = Directions.West;
                    }
                }
                else if (direction == Directions.West)
                {
                    if (bounds.X <= nextPosition.X)
                    {
                        nextPosition.X--;
                    }
                    else
                    {
                        nextPosition.Y--;
                        bounds.X--;
                        direction = Directions.North;
                    }
                }
                else if (direction == Directions.North)
                {
                    if (bounds.Y <= nextPosition.Y)
                    {
                        nextPosition.Y--;
                    }
                    else
                    {
                        nextPosition.X++;

                        bounds.Y--;
                        direction = Directions.East;
                    }
                }
                number--;
            }

            return nextPosition;
        }

        private enum Directions
        {
            North,
            East,
            South,
            West
        }
    }
}
