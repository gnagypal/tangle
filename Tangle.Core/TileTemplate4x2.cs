namespace Tangle.Core
{
    using System.Drawing;

    /// <summary>
    /// 4 border and 2 points on each border
    /// </summary>
    public class TileTemplate4X2 : TileTemplateBase
    {
        public TileTemplate4X2() 
        {
            var tileBorders = new[]
            {   
                    new TileTemplateBorder(
                        Direction.Top,                                      // Top border:       left, right
                        new[] { 0, 1 },                                     // Junction points color indexes
                        new[] { new Point(-20, -40), new Point(20, -40) }), // Junction points coordinates for drawing

                    new TileTemplateBorder(
                        Direction.Right,                                    // Right border:     top, bottom
                        new[] { 2, 3 },                                     // Junction points color indexes
                        new[] { new Point(40, -20), new Point(40, 20) }),   // Junction points coordinates for drawing

                    new TileTemplateBorder(
                        Direction.Bottom,                                   // Bottom border:    righ, left
                        new[] { 0, 3 },                                     // Junction points color indexes
                        new[] { new Point(20, 40), new Point(-20, 40) }),   // Junction points coordinates for drawing

                    new TileTemplateBorder(
                        Direction.Left,                                     // Left border:      bottop, top
                        new[] { 1, 2 },                                     // Junction points color indexes
                        new[] { new Point(-40, 20), new Point(-40, -20) })  // Junction points coordinates for drawing
                };

            var labelRectangle = new Rectangle(-40, -40, 20, 20);

            var frameRectangle = new Rectangle(-40, -40, 80, 80);

            this.Initialize(tileBorders, labelRectangle, frameRectangle);
        }
    }
}
