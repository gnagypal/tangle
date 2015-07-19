namespace Tangle.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Statuses of all cells on the table.
    /// </summary>
    public class TileSequence : List<TileSequenceItem>, ICloneable 
    {
        public TileSequence(int count) : base(count)
        {
            for (int i = 0; i < count; i++)
            {
                this.Add(new TileSequenceItem());
            }
        }

        public object Clone()
        {
            // deep clone
            var clone = new TileSequence(this.Count);
            for (int i = 0; i < this.Count; i++)
            {
                clone[i] = (TileSequenceItem)this[i].Clone();
            }

            return clone;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < this.Count; i++)
            {
                if (sb.Length != 0)
                {
                    sb.Append(",");
                }

               sb.Append(this[i]);
            }

            return sb.ToString();
        }
    }
}
