namespace Tangle.Core
{
    using System;

    public static class DirectionExtentensions
    {
        public static Direction GetOppositeDirection(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Bottom: return Direction.Top;
                case Direction.Top: return Direction.Bottom;
                case Direction.Left: return Direction.Right;
                case Direction.Right: return Direction.Left;
                default:
                    throw new ArgumentException(string.Format("The '{0}' value of direction is invalid!", direction), "direction");
            }
        }
    }
}
