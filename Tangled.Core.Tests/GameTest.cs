namespace Tangled.Core.Tests
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Imaging;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tangled.Core;

    [TestClass]
    public class GameTest
    {
        private Graphics graphics;

        private Bitmap bitmap;

        [TestMethod]
        public void TestMethod3X3()
        {
            this.CreateBitmap();

            var t = new PaintedTileSetFactory().Create9();
            var tableLayout = new TableLayout(t, 9, 3);
            var st = this.CallSearch(tableLayout, false);
            var headerEndY = this.DrawPaintedTilesHeader(t, 100, 100, 5);
            this.DrawResultTileSequences(tableLayout, 100, headerEndY + 100, 3, 100, st.Elapsed);

            this.SaveBitmap("test3X3.png");
        }

        [TestMethod]
        public void TestMethod4X2()
        {
            this.CreateBitmap();

            var t = new PaintedTileSetFactory().Create9();
            var tableLayout = new TableLayout(t, 8, 4);
            var st = this.CallSearch(tableLayout, false);
            var headerEndY = this.DrawPaintedTilesHeader(t, 100, 100, 5);
            this.DrawResultTileSequences(tableLayout, 100, headerEndY + 100, 4, 100, st.Elapsed);

            this.SaveBitmap("test4X2.png");
        }

        [TestMethod]
        public void TestMethod4X4()
        {
            this.CreateBitmap();

            var t = new PaintedTileSetFactory().Create16();
            var tableLayout = new TableLayout(t, 16, 4);
            var st = this.CallSearch(tableLayout, false);
            var headerEndY = this.DrawPaintedTilesHeader(t, 100, 100, 5);
            this.DrawResultTileSequences(tableLayout, 100, headerEndY + 100, 4, 100, st.Elapsed);

            this.SaveBitmap("test4X4.png");
        }

        [TestMethod]
        public void TestMethod5X5()
        {
            this.CreateBitmap();

            var t = new PaintedTileSetFactory().Create25();
            var tableLayout = new TableLayout(t, 25, 5);
            var st = this.CallSearch(tableLayout, false);
            var headerEndY = this.DrawPaintedTilesHeader(t, 100, 100, 5);
            this.DrawResultTileSequences(tableLayout, 100, headerEndY + 100, 4, 100, st.Elapsed);

            this.SaveBitmap("test5X5.png");
        }

        private void CreateBitmap()
        {
            this.bitmap = new Bitmap(Convert.ToInt32(3000), Convert.ToInt32(3000), PixelFormat.Format32bppArgb);
            this.graphics = Graphics.FromImage(this.bitmap);
            this.graphics.Clear(Color.GhostWhite);
        }

        private Stopwatch CallSearch(TableLayout tableLayout, bool exitWhenFound)
        {
            var st = new Stopwatch();
            st.Start();
            tableLayout.Search(exitWhenFound);
            st.Stop();
            return st;
        }

        private void DrawResultTileSequences(TableLayout tableLayout, int startX, int startY, int tableColumnCount, int gap, TimeSpan elapsed)
        {
            Debug.WriteLine("tableLayout.ResultTileSequences.Count: {0}", tableLayout.ResultTileSequences.Count);

            Font font = new Font(FontFamily.GenericSansSerif, 20);
            SolidBrush brush = new SolidBrush(Color.Black);
            this.graphics.DrawString(
                string.Format(
                    "A táblán összesen {0} kártya lehet, {1} oszlopban. A keresés {3:0.000} másodperc alatt {2} db elrendezést talált; \n {4} alkalommal próbált meg letenni egy kártyát, és az {5} alkalommal sikerült.", 
                    tableLayout.TileCountOnTable, 
                    tableLayout.ColumnCount, 
                    tableLayout.ResultTileSequences.Count,
                    elapsed.TotalSeconds,
                    tableLayout.PutTileTryCount,
                    tableLayout.PutTileSuccessCount), 
                font, 
                brush, 
                10, 
                startY - 120);

            this.graphics.TranslateTransform(startX, startY);

            for (int i = 0; i < tableLayout.ResultTileSequences.Count; i++)
            {
                var tileSequence = tableLayout.ResultTileSequences[i];

                Debug.WriteLine("i: {0}, tileSequence: {1}", i, tileSequence);
                var size = tableLayout.DrawTileSequence(this.graphics, tileSequence);

                int x = (i + 1) % tableColumnCount;
                int y = (i + 1) / tableColumnCount;

                this.graphics.ResetTransform();
                this.graphics.TranslateTransform(startX + ((size.Width + gap) * x), startY + ((size.Height + gap) * y));
            }
        }

        private int DrawPaintedTilesHeader(PaintedTiles t, int startX, int startY, int columnCount)
        {
            Font font = new Font(FontFamily.GenericSansSerif, 20);
            SolidBrush brush = new SolidBrush(Color.Black);
            this.graphics.DrawString(string.Format("{0} db választható kártya:", t.Count), font, brush, 10, 10);

            this.graphics.TranslateTransform(startX, startY);

            for (int i = 0; i < t.Count; i++)
            {
                int x = i % 5;
                int y = i / 5;
                this.graphics.ResetTransform();
                this.graphics.TranslateTransform(
                    startX + (t[i].TileTemplate.FrameRectangle.Width * x),
                    startY + (t[i].TileTemplate.FrameRectangle.Height * y));
                t[i].Rotation = Rotation.X000;
                t[i].DrawTile(this.graphics);
            }

            this.graphics.ResetTransform();

            int headerHeight = t[0].TileTemplate.FrameRectangle.Height * ((t.Count / columnCount) + 1);
            int endY = startY + headerHeight;
            return endY;
        }

        private void SaveBitmap(string name)
        {
            this.bitmap.Save(name, ImageFormat.Png);
        }
    }
}
