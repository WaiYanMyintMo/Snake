using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public readonly struct Coordinate
    {
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; }
        public int Y { get; }
    }
}
