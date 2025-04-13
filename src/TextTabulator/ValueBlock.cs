using System.Collections.Generic;

namespace TextTabulator
{
    public interface IValueBlock
    {
        IList<string> Lines { get; }

        Dimension Size { get; }
    }

    public class ValueBlock : IValueBlock
    {
        public IList<string> Lines { get; }

        public Dimension Size { get; }

        public ValueBlock(IList<string> lines, Dimension size)
        {
            Lines = lines;
            Size = size;
        }
    }
}
