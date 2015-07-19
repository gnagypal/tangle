namespace Tangled.Core
{
    using System.Drawing;

    public class PaintedTileSetFactory
    {
        public PaintedTiles Create9()
        {
            var t = new TileTemplate4X2();
            var result = new PaintedTiles
            {
                new PaintedTile(0, t, new[] { Color.Red, Color.Green, Color.Yellow, Color.Blue }),
                new PaintedTile(1, t, new[] { Color.Red, Color.Blue, Color.Yellow, Color.Green }),
                new PaintedTile(2, t, new[] { Color.Green, Color.Blue, Color.Yellow, Color.Red }),
                new PaintedTile(3, t, new[] { Color.Blue, Color.Red, Color.Green, Color.Yellow }),
                new PaintedTile(4, t, new[] { Color.Red, Color.Blue, Color.Green, Color.Yellow }),
                new PaintedTile(5, t, new[] { Color.Yellow, Color.Red, Color.Green, Color.Blue }),
                new PaintedTile(6, t, new[] { Color.Yellow, Color.Green, Color.Red, Color.Blue }),
                new PaintedTile(7, t, new[] { Color.Yellow, Color.Green, Color.Blue, Color.Red }),
                new PaintedTile(8, t, new[] { Color.Green, Color.Yellow, Color.Blue, Color.Red })
            };
            return result;
        }

        public PaintedTiles Create16()
        {
            var t = new TileTemplate4X2();
            var result = new PaintedTiles
            {
                new PaintedTile(1, t, new[] { Color.Blue, Color.Green, Color.Red, Color.Yellow }),
                new PaintedTile(2, t, new[] { Color.Blue, Color.Green, Color.Yellow, Color.Red }),
                new PaintedTile(3, t, new[] { Color.Blue, Color.Red, Color.Green, Color.Yellow }),
                new PaintedTile(4, t, new[] { Color.Blue, Color.Red, Color.Yellow, Color.Green }),
                new PaintedTile(5, t, new[] { Color.Blue, Color.Yellow, Color.Red, Color.Green }),
                new PaintedTile(6, t, new[] { Color.Green, Color.Blue, Color.Yellow, Color.Red }),
                new PaintedTile(7, t, new[] { Color.Green, Color.Red, Color.Yellow, Color.Blue }),    
                new PaintedTile(8, t, new[] { Color.Green, Color.Yellow, Color.Blue, Color.Red }),
                new PaintedTile(9, t, new[] { Color.Green, Color.Yellow, Color.Red, Color.Blue }),
                new PaintedTile(10, t, new[] { Color.Red, Color.Blue, Color.Green, Color.Yellow }),
                new PaintedTile(11, t, new[] { Color.Red, Color.Blue, Color.Yellow, Color.Green }),
                new PaintedTile(12, t, new[] { Color.Red, Color.Green, Color.Blue, Color.Yellow }),
                new PaintedTile(13, t, new[] { Color.Red, Color.Green, Color.Yellow, Color.Blue }),
                new PaintedTile(14, t, new[] { Color.Red, Color.Yellow, Color.Blue, Color.Green }),
                new PaintedTile(15, t, new[] { Color.Yellow, Color.Green, Color.Blue, Color.Red }),
                new PaintedTile(16, t, new[] { Color.Yellow, Color.Green, Color.Red, Color.Blue }),
            };
            return result;
        }

        public PaintedTiles Create25()
        {
            var t = new TileTemplate4X2();

            // Coloring from http://www.dotsphinx.com/media/games/rubik/tangle/img/tangle.gif
            var result = new PaintedTiles
            {
                new PaintedTile(1, t, new[] { Color.Blue, Color.Green, Color.Red, Color.Yellow }),
                new PaintedTile(2, t, new[] { Color.Blue, Color.Green, Color.Yellow, Color.Red }),
                new PaintedTile(3, t, new[] { Color.Blue, Color.Red, Color.Green, Color.Yellow }),
                new PaintedTile(4, t, new[] { Color.Blue, Color.Red, Color.Yellow, Color.Green }),
                new PaintedTile(5, t, new[] { Color.Blue, Color.Yellow, Color.Green, Color.Red }),
                new PaintedTile(6, t, new[] { Color.Blue, Color.Yellow, Color.Red, Color.Green }),

                new PaintedTile(7, t, new[] { Color.Green, Color.Blue, Color.Yellow, Color.Red }),
                new PaintedTile(8, t, new[] { Color.Green, Color.Blue, Color.Red, Color.Yellow }),
                new PaintedTile(9, t, new[] { Color.Green, Color.Red, Color.Yellow, Color.Blue }),    // These items are the same
                new PaintedTile(10, t, new[] { Color.Green, Color.Red, Color.Yellow, Color.Blue }),   // These items are the same
                new PaintedTile(11, t, new[] { Color.Green, Color.Red, Color.Blue, Color.Yellow }),
                new PaintedTile(12, t, new[] { Color.Green, Color.Yellow, Color.Blue, Color.Red }),
                new PaintedTile(13, t, new[] { Color.Green, Color.Yellow, Color.Red, Color.Blue }),
                
                new PaintedTile(14, t, new[] { Color.Red, Color.Blue, Color.Green, Color.Yellow }),
                new PaintedTile(15, t, new[] { Color.Red, Color.Blue, Color.Yellow, Color.Green }),
                new PaintedTile(16, t, new[] { Color.Red, Color.Green, Color.Blue, Color.Yellow }),
                new PaintedTile(17, t, new[] { Color.Red, Color.Green, Color.Yellow, Color.Blue }),
                new PaintedTile(18, t, new[] { Color.Red, Color.Yellow, Color.Blue, Color.Green }),
                new PaintedTile(19, t, new[] { Color.Red, Color.Yellow, Color.Green, Color.Blue }),

                new PaintedTile(20, t, new[] { Color.Yellow, Color.Green, Color.Blue, Color.Red }),
                new PaintedTile(21, t, new[] { Color.Yellow, Color.Green, Color.Red, Color.Blue }),
                new PaintedTile(22, t, new[] { Color.Yellow, Color.Blue, Color.Green, Color.Red }),
                new PaintedTile(23, t, new[] { Color.Yellow, Color.Blue, Color.Red, Color.Green }),
                new PaintedTile(24, t, new[] { Color.Yellow, Color.Red, Color.Green, Color.Blue }),
                new PaintedTile(25, t, new[] { Color.Yellow, Color.Red, Color.Blue, Color.Green })
            };
            return result;
        }
    }
}
