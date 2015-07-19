namespace Tangle.Core
{
    using System.Drawing;
    
    /// <summary>
    /// Border of a painted tile
    /// </summary>
    public class PaintedTileBorder
    {
        public PaintedTileBorder(int pointCount)
        {
            this.PointColors = new Color[pointCount];
            this.PointCoordinates = new Point[pointCount];
        }

        /// <summary>
        /// Points on the current border
        /// The values of the array are the colors of the points.
        /// The points are in clockwise direction, respectively.
        /// </summary>
        public Color[] PointColors { get; private set; }

        /// <summary>
        /// Points coordinates on the current border, they are only neccessary for drawing.
        /// The points are in clockwise direction, respectively.
        /// </summary>
        public Point[] PointCoordinates { get; private set; }

        public int PointCount
        {
            get { return this.PointColors.Length; }
        }
    }
}
