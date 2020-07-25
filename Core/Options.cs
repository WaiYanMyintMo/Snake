using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class Options
    {
        public int? RandomSeed { get; set; } = null;

        public Point Size { get; set; } = (50, 20);

        public Point? Apple { get; set; } = null;

        public int? AppleX { get; set; } = null;

        public int? AppleY { get; set; } = null;

#pragma warning disable CA2227 // Collection properties should be read only
        public List<Point>? Snake { get; set; } = null;
#pragma warning restore CA2227 // Collection properties should be read only
    }
}
