using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class World
    {
        public World() : this((50, 50))
        {
        }

        public World(Coordinate size)
        {
            Size = size;
        }

        public Coordinate Size { get; }

    }
}
