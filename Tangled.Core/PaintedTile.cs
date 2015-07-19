namespace Tangled.Core
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    /// Painted tile instance, based on TileTemplateBase.
    /// </summary>
    public class PaintedTile
    {
        private readonly TileTemplateBase tileTemplate;
                
        public PaintedTile(int id, TileTemplateBase tileTemplate, Color[] colorConfiguration)
        {
            this.tileTemplate = tileTemplate;
            this.Id = id;
            this.ColorConfiguration = colorConfiguration;
            this.TileBorders = new PaintedTileBorder[4];
            this.InitializeTileBorders();
        }

        public Color[] ColorConfiguration { get; private set; }

        public PaintedTileBorder[] TileBorders { get; private set; }

        public int Id { get; set; }

        /// <summary>
        /// Absolute value to the top direction of table
        /// </summary>
        public Rotation Rotation { get; set; }

        public TileTemplateBase TileTemplate
        {
            get
            {
                return this.tileTemplate;
            }
        }

        /// <summary>
        /// Refresh the requested rotated PaintedTileBorder instance with point colors and point coordinates.  
        /// </summary>
        /// <param name="tableDirection"></param>
        /// <param name="forDrawing"></param>
        /// <returns></returns>
        public PaintedTileBorder GetRefreshedTileBorder(Direction tableDirection, bool forDrawing = false)
        {
            var tileBorder = this.GetTileBorder(tableDirection);

            Direction templateDirection = this.GetUnrotatedDirection(tableDirection);
            var templateBorder = this.tileTemplate.GetTileTemplateBorder(templateDirection);
             
            for (int i = 0; i < templateBorder.PointColorIds.Length; i++)
            {
                int colorId = templateBorder.PointColorIds[i];
                tileBorder.PointColors[i] = this.GetColorFromConfiguration(colorId);
            }

            if (forDrawing)
            {
                for (int i = 0; i < templateBorder.PointCoordinates.Length; i++)
                {
                    tileBorder.PointCoordinates[i].X = templateBorder.PointCoordinates[i].X;
                    tileBorder.PointCoordinates[i].Y = templateBorder.PointCoordinates[i].Y;
                }
            }

            return tileBorder;
        }

        /// <summary>
        /// Get the direction inborder the template, calculated from the rotation of the tile and the direction on the table
        /// </summary>
        /// <param name="rotatedDirection"></param>
        /// <returns></returns>
        public Direction GetUnrotatedDirection(Direction rotatedDirection)
        {
            int unrotatedDirection = (int)rotatedDirection - (int)this.Rotation;

            // The rotation value is positive number, therefore the unrotated direction can be negative, 
            // which should be corrected to valid direction range by 360 degree
            if (unrotatedDirection < 0)   
            {
                unrotatedDirection = unrotatedDirection + (int)Rotation.X360;
            }

            if (unrotatedDirection < 0 && 3 <= unrotatedDirection)
            {
                throw new ApplicationException(string.Format("UnrotatedDirection value error! rotatedDirection: '{0}', unrotatedDirection: '{1}'", unrotatedDirection, unrotatedDirection));
            }

            return (Direction)unrotatedDirection;
        }

        public bool IsMatch(Direction direction, PaintedTile neighbour)
        {
            var currentBorder = this.GetRefreshedTileBorder(direction);
            var neighbourBorder = neighbour.GetRefreshedTileBorder(direction.GetOppositeDirection());

            // checking the number of points of both borders
            if (currentBorder.PointColors.Length != neighbourBorder.PointColors.Length)
            {
                return false;
            }

            // checking the colors of points on both borders
            for (int currentIndex = 0; currentIndex < currentBorder.PointColors.Length; currentIndex++)
            {
                var currentColor = currentBorder.PointColors[currentIndex];
                
                // points represented in clockwise directon, but the matching point at the neighbour border is in reserved direction
                int neighbourIndex = currentBorder.PointColors.Length - currentIndex - 1; 
                var neighbourColor = neighbourBorder.PointColors[neighbourIndex];

                if (currentColor != neighbourColor)
                {
                    return false;
                }
            }

            return true;
        }

        public void DrawTile(Graphics graph)
        {
            var pointsByColors = this.GetAllBordersPoints();

            var containerState = graph.BeginContainer();
            graph.RotateTransform(90 * (int)this.Rotation);

            this.DrawPoints(graph, pointsByColors);
            this.DrawText(graph);

            graph.EndContainer(containerState);
        }

        private void DrawText(Graphics graph)
        {
            Font font = new Font(FontFamily.GenericSansSerif, 10);
            SolidBrush brush = new SolidBrush(Color.Black);
            graph.DrawString(this.Id.ToString(), font, brush, this.tileTemplate.LabelRectangle);
        }

        private void DrawPoints(Graphics graph, Dictionary<Color, List<Point>> pointsByColors)
        {
            foreach (var color in pointsByColors.Keys)
            {
                var p = new Pen(color, 4);
                var points = pointsByColors[color];
                if (points.Count != 2)
                {
                    throw new ApplicationException(string.Format("Only two points are allowed to belong to one color in a tile! Color: '{0}', points.Count: '{1}'", color, points.Count));
                }

                graph.DrawLine(p, points[0], points[1]);
            }
        }

        private Dictionary<Color, List<Point>> GetAllBordersPoints()
        {
            var pointsByColors = new Dictionary<Color, List<Point>>();

            foreach (Direction tableDirection in Enum.GetValues(typeof(Direction)))
            {
                var tileBorder = this.GetRefreshedTileBorder(tableDirection, true);

                for (int i = 0; i < tileBorder.PointColors.Length; i++)
                {
                    var currentColor = tileBorder.PointColors[i];
                    List<Point> pointList;
                    if (!pointsByColors.ContainsKey(currentColor))
                    {
                        pointList = new List<Point>();
                        pointsByColors.Add(currentColor, pointList);
                    }
                    else
                    {
                        pointList = pointsByColors[currentColor];
                    }

                    pointList.Add(tileBorder.PointCoordinates[i]);
                }
            }

            return pointsByColors;
        }

        private Color GetColorFromConfiguration(int colorId)
        {
            if (colorId < 0 || this.ColorConfiguration.Length <= colorId)
            {
                throw new ArgumentOutOfRangeException(
                    colorId.ToString(), 
                    "colorId", 
                    string.Format("The caller references and invalid colorId! The value must between 0 and ColorConfiguration.Length - 1 (0...{0})", this.ColorConfiguration.Length - 1));
            }

            return this.ColorConfiguration[colorId];
        }

        private PaintedTileBorder GetTileBorder(Direction tableDirection)
        {
            int index = (int)tableDirection;
            return this.TileBorders[index];
        }

        private void InitializeTileBorders()
        {
            foreach (Direction tableDirection in Enum.GetValues(typeof(Direction)))
            {
                var templateBorder = this.tileTemplate.GetTileTemplateBorder(tableDirection);
                this.SetTileBorder(tableDirection, new PaintedTileBorder(templateBorder.PointCount));
            }
        }

        private void SetTileBorder(Direction tableDirection, PaintedTileBorder tileBorder)
        {
            int index = (int)tableDirection;
            this.TileBorders[index] = tileBorder;
        }
    }
}
