namespace Tangle.Core
{
    using System;
    
    /// <summary>
    /// Status of a sigle cell on the table: painted tile id and rotation.
    /// </summary>
    public class TileSequenceItem : ICloneable
    {
        public int TileId { get; set; }

        public Rotation Rotation { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}", this.TileId, this.Rotation);
        }
    }
}
