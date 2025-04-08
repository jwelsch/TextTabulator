using System;
using System.Collections.Generic;
using System.Text;

namespace TextTabulator
{
    public class Dimension
    {
        public int Width { get; }

        public int Height { get; }

        public Dimension(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
