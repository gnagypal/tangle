namespace Tangled.Core
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class TableLayout
    {
        private readonly PaintedTiles paintedTiles;

        private readonly int[][] availableTilesInLevels;

        private readonly int tileCountOnTable;

        private readonly int columnCount;

        /// <summary>
        /// Physical layout of the a x b table: table tile coordinates
        /// <remarks>
        /// Sample layout for 3 x 3 table:
        /// 0 -> 0:0   1 -> 1:0   2 -> 2:0
        /// 3 -> 0:1   4 -> 1:1   5 -> 2:1
        /// 6 -> 0:2   7 -> 1:2   8 -> 2:2
        /// </remarks>
        /// </summary>
        private readonly Point[] physicalLayout;

        private readonly List<TileSequence> resultTileSequences;

        private readonly TileSequence actualTileSequence;

        private long putTileSuccessCount;
       
        private long putTileTryCount;

        public TableLayout(PaintedTiles paintedTiles, int tileCountOnTable, int columnCount)
        {
            if (paintedTiles == null)
            {
                throw new ArgumentNullException("paintedTiles");
            }

            if (paintedTiles.Count < tileCountOnTable)
            {
                throw new ArgumentOutOfRangeException("tileCountOnTable", tileCountOnTable, "Number of places on the table must smaller or equal than number of available tiles (paintedTiles)!");    
            }

            this.paintedTiles = paintedTiles;
            this.tileCountOnTable = tileCountOnTable;
            this.columnCount = columnCount;

            this.physicalLayout = this.BuildPhysicalLayout();

            // preallocating arrays to store available tiles at every single recursion level independently
            this.availableTilesInLevels = new int[this.tileCountOnTable][];
            for (int i = 0; i < this.tileCountOnTable; i++)
            {
                // number of available tiles = number of paintedTiles - already placed tiles on table at the level of recursion
                this.availableTilesInLevels[i] = new int[this.PaintedTileCount - i];
            }

            this.actualTileSequence = new TileSequence(this.tileCountOnTable);
            this.resultTileSequences = new List<TileSequence>();
        }

        public List<TileSequence> ResultTileSequences
        {
            get
            {
                return this.resultTileSequences;
            }
        }

        public int TileCountOnTable
        {
            get
            {
                return this.tileCountOnTable;
            }
        }

        public int ColumnCount
        {
            get
            {
                return this.columnCount;
            }
        }

        public long PutTileSuccessCount
        {
            get
            {
                return this.putTileSuccessCount;
            }
        }

        public long PutTileTryCount
        {
            get
            {
                return this.putTileTryCount;
            }
        }

        private int PaintedTileCount
        {
            get
            {
                return this.paintedTiles.Count;
            }
        }

        public Size DrawTileSequence(Graphics graph, TileSequence tileSequenceToDraw)
        {
            var tileSize = new Size();

            for (int i = 0; i < tileSequenceToDraw.Count; i++)
            {
                var containerState = graph.BeginContainer();

                var paintedTile = this.paintedTiles[tileSequenceToDraw[i].TileId];
                tileSize = new Size(paintedTile.TileTemplate.FrameRectangle.Width, paintedTile.TileTemplate.FrameRectangle.Height);

                graph.TranslateTransform(
                    this.physicalLayout[i].X * tileSize.Width,
                    this.physicalLayout[i].Y * tileSize.Height);

                paintedTile.Rotation = tileSequenceToDraw[i].Rotation;
                paintedTile.DrawTile(graph);

                graph.EndContainer(containerState);
            }

            return new Size(
                tileSize.Width * this.columnCount,
                tileSize.Height * ((int)Math.Floor((double)tileSequenceToDraw.Count / (double)this.columnCount))); 
        }

        public bool Search(bool exitWhenFound)
        {
            this.resultTileSequences.Clear();
            this.putTileSuccessCount = 0;
            this.putTileTryCount = 0;

            // set the initial order of tile id-s at level 0
            for (int i = 0; i < this.PaintedTileCount; i++)
            {
                this.availableTilesInLevels[0][i] = i;
            }

            // start search from the top of the tree
            this.SearchRecursive(0, exitWhenFound);
            return this.resultTileSequences.Count > 0;
        }

        private bool SearchRecursive(int level, bool exitWhenFound)
        {
            for (int i = 0; i < this.availableTilesInLevels[level].Length; i++)
            {
                int currentTileId = this.availableTilesInLevels[level][i];

                for (var r = Rotation.X000; r < Rotation.X360; r++)
                {
                    if (this.TryToPutTile(level, currentTileId, r))
                    {
                        // if the final level is reached in the tree, then we should return
                        if (this.IsAtLeafLevelInSearchTree(level))
                        {
                            return true;
                        }

                        // otherwise we go deeper in the tree
                        this.CopyArrayWithoutMarkedElement(
                            this.availableTilesInLevels[level],
                            this.availableTilesInLevels[level + 1],
                            i);

                        bool found = this.SearchRecursive(level + 1, exitWhenFound);
                        if (found && exitWhenFound)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private Point[] BuildPhysicalLayout()
        {
            var result = new Point[this.tileCountOnTable];
            for (int i = 0; i < this.tileCountOnTable; i++)
            {
                int x = i % this.columnCount;   // x coordinate resets periodically when columnCount is reached
                int y = i / this.columnCount;   // y coordinate within the row
                result[i] = new Point(x, y);
            }

            return result;
        }

        private bool TryToPutTile(int level, int currentTileId, Rotation rotation)
        {
            this.putTileTryCount++;
            this.actualTileSequence[level].TileId = currentTileId;
            this.actualTileSequence[level].Rotation = rotation;

            var currentPaintedTile = this.paintedTiles[currentTileId];
            currentPaintedTile.Rotation = rotation;

            bool isOk = true;

            /*  The tiles are put down left to right, top to bottom order.
                We only check the left and/or top borders of the current tile, because the 
                right and bottom borders are checked by the subsequent tiles later */

            if (this.IsLeftNeighbourCheckNecessary(level))
            {
                isOk = this.CheckLeftNeighbour(level, currentTileId);
            }

            if (isOk && this.IsTopNeighbourCheckNecessary(level))
            {
                isOk = this.CheckTopNeighbour(level, currentTileId);
            }

            // if the previous checkings are ok, and we are at leaf level in the tree then we found a solution
            if (isOk && this.IsAtLeafLevelInSearchTree(level))
            {
                // to save the actualTileSequence we should make a copy, because it is a reference object
                this.resultTileSequences.Add((TileSequence)this.actualTileSequence.Clone());
            }

            if (isOk)
            {
                this.putTileSuccessCount++;
            }

            return isOk;
        }

        private bool CheckTopNeighbour(int level, int currentTileId)
        {
            var currentPaintedTile = this.paintedTiles[currentTileId];

            // neigbour is in the previous row
            int neighbourTileId = this.actualTileSequence[level - this.columnCount].TileId;
            var neighbourPaintedTile = this.paintedTiles[neighbourTileId];

            return currentPaintedTile.IsMatch(Direction.Top, neighbourPaintedTile);
        }

        private bool CheckLeftNeighbour(int level, int currentTileId)
        {
            var currentPaintedTile = this.paintedTiles[currentTileId];

            // neigbour is the previous element
            int neighbourTileId = this.actualTileSequence[level - 1].TileId;
            var neighbourPaintedTile = this.paintedTiles[neighbourTileId];

            return currentPaintedTile.IsMatch(Direction.Left, neighbourPaintedTile);
        }

        private bool IsTopNeighbourCheckNecessary(int level)
        {
            // Check is only neccessary when earlier tile exists in this direction, according to the layout
            return this.physicalLayout[level].Y > 0;
        }

        private bool IsLeftNeighbourCheckNecessary(int level)
        {
            // Check is only neccessary when earlier tile exists in this direction, according to the layout
            return this.physicalLayout[level].X > 0;
        }

        private bool IsAtLeafLevelInSearchTree(int level)
        {   // We are at leaf level in the search tree 
            // when recursion level points to the latest item in this.actualTileSequence array
            return level == this.tileCountOnTable - 1;
        }

        private void CopyArrayWithoutMarkedElement(int[] source, int[] destination, int markedSourceElementIndex)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (destination == null)
            {
                throw new ArgumentNullException("source");
            }

            if (source.Length != destination.Length + 1)
            {
                throw new ArgumentException(string.Format("The source array length must larger with one element than the destination array length. source.Length: '{0}', destination.Length: '{1}' ", source.Length, destination.Length));
            }

            if (markedSourceElementIndex < 0 || source.Length <= markedSourceElementIndex)
            {
                throw new ArgumentOutOfRangeException("markedSourceElementIndex", markedSourceElementIndex, string.Format("source.Length: '{0}'", source.Length));
            }

            int j = 0;
            for (int i = 0; i < source.Length; i++)
            {
                if (i != markedSourceElementIndex)
                {
                    destination[j] = source[i];
                    j++;
                }
            }
        }
    }
}
