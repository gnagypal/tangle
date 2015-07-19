namespace Tangle.Core
{
    using System;
    using System.Drawing;

    /// <summary>
    /// The "white" tile template with configurable border points
    /// </summary>
    public abstract class TileTemplateBase
    {
        /// <summary>
        /// The 4 borders of the tile template, they are represented in clockwise direction, starts at the top
        /// </summary>
        public TileTemplateBorder[] TileBorders { get; private set; }

        /// <summary>
        /// Rectangle of tile label for drawing
        /// </summary>
        public Rectangle LabelRectangle { get; private set; }

        /// <summary>
        /// Outer rectangle of tile for drawing
        /// </summary>
        public Rectangle FrameRectangle { get; private set; }

        public TileTemplateBorder GetTileTemplateBorder(Direction directionWithinTemplate)
        {
            int index = (int)directionWithinTemplate;   
            return this.TileBorders[index];
        }

        protected void Initialize(TileTemplateBorder[] tileBorders, Rectangle labelRectangle, Rectangle frameRectangle)
        {
            this.TileBorders = tileBorders;
            this.LabelRectangle = labelRectangle;
            this.FrameRectangle = frameRectangle;

            if (this.TileBorders == null || this.TileBorders.Length != 4)
            {
                throw new ArgumentException("tileBorders");
            }
        }
    }
}
