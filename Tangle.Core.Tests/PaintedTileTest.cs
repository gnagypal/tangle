namespace Tangle.Core.Tests
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tangle.Core;

    [TestClass]
    public class PaintedTileTest
    {
        [TestMethod]
        public void DrawingTest()
        {
            var template = new TileTemplate4X2();
            var tile1 = new PaintedTile(1, template, new Color[] { Color.Blue, Color.Green, Color.Red, Color.Yellow });
            var tile2 = new PaintedTile(2, template, new Color[] { Color.Blue, Color.Green, Color.Red, Color.Yellow });

            Bitmap bitmap = new Bitmap(Convert.ToInt32(600), Convert.ToInt32(600), PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bitmap);
            Pen pen = new Pen(Color.Black, 2);
            g.Clear(Color.WhiteSmoke);

            g.DrawRectangle(pen, 10, 10, 10, 10);
            g.DrawRectangle(pen, 580, 580, 10, 10);

            g.TranslateTransform(100, 100);
            tile1.DrawTile(g);

            tile2.Rotation = Rotation.X000;
            g.TranslateTransform(80, 0);
            tile2.DrawTile(g);

            tile2.Rotation = Rotation.X090;
            g.TranslateTransform(80, 0);
            tile2.DrawTile(g);

            tile2.Rotation = Rotation.X180;
            g.TranslateTransform(80, 0);
            tile2.DrawTile(g);

            tile2.Rotation = Rotation.X270;
            g.TranslateTransform(80, 0);
            tile2.DrawTile(g);

            bitmap.Save(@"TileTest.png", ImageFormat.Png);
        }

        [TestMethod]
        public void MatchTest()
        {
            var t = new PaintedTileSetFactory().Create9();

            Assert.IsTrue(t[0].IsMatch(Direction.Bottom, t[3]));
            Assert.IsTrue(t[0].IsMatch(Direction.Right, t[1]));
            Assert.IsTrue(t[0].IsMatch(Direction.Right, t[2]));
            Assert.IsFalse(t[0].IsMatch(Direction.Right, t[8]));

            t[8].Rotation = Rotation.X090;
            Assert.IsTrue(t[1].IsMatch(Direction.Top, t[8]));

            t[1].Rotation = Rotation.X090;
            t[8].Rotation = Rotation.X180;

            Assert.IsTrue(t[1].IsMatch(Direction.Right, t[8]));

            t[1].Rotation = Rotation.X270;
            t[8].Rotation = Rotation.X000;

            Assert.IsTrue(t[1].IsMatch(Direction.Left, t[8]));
            Assert.IsTrue(t[8].IsMatch(Direction.Right, t[1]));

            t[8].Rotation = Rotation.X180;
            Assert.IsFalse(t[8].IsMatch(Direction.Right, t[1]));
        }
    }
}
