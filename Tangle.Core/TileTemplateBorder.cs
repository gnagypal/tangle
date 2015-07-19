namespace Tangle.Core
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Border of a tile template
    /// </summary>
    public class TileTemplateBorder
    {
        public TileTemplateBorder(Direction directionWithinTemplate, int[] pointColorIds, Point[] pointCoordinates)
        {
            if (pointColorIds == null)
            {
                throw new ArgumentNullException("pointColorIds");
            }

            if (pointCoordinates == null)
            {
                throw new ArgumentNullException("pointCoordinates");
            }

            if (pointColorIds.Length != pointCoordinates.Length)
            {
                throw new ArgumentException("pointCoordinates and pointColorIds length are different!");
            }

            this.DirectionWithinTemplate = directionWithinTemplate;
            this.PointColorIds = pointColorIds;
            this.PointCoordinates = pointCoordinates;
        }

        /// <summary>
        /// Used for tracing.
        /// </summary>
        public Direction DirectionWithinTemplate { get; private set; }

        /// <summary>
        /// Points colors on the current border.
        /// The values of the array are the color indexes of the points.
        /// The points are in clockwise direction, respectively.
        /// Used for drawing and for calculate the color matching with the neighbour tile.
        /// </summary>
        public int[] PointColorIds { get; private set; }

        /// <summary>
        /// Points coordinates on the current border, they are only neccessary for drawing.
        /// The points are in clockwise direction, respectively.
        /// </summary>
        public Point[] PointCoordinates { get; private set; }

        public int PointCount 
        { 
            get { return this.PointColorIds.Length; } 
        }
    }
}
