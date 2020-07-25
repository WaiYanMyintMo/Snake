using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    public class Options
    {
        [Option('v', "verbose", Required = false, HelpText = "Generate extra details at end of program")]
        public bool Verbose { get; set; } = false;

        [Option('t', "time", Required = false, HelpText = "Timespan per update in milliseconds (double)")]
        public double MillisecondsPerUpdate { get; set; } = 150.0;

        [Option('s', "seed", Required = false, HelpText = "Seed for Random when generating apples")]
        public int? RandomSeed { get; set; } = null;

        [Option('x', "x-length", Required = false, HelpText = "X coordinate length of world")]
        public int X { get; set; } = 50;

        [Option('y', "y-length", Required = false, HelpText = "Y coordinate length of world")]
        public int Y { get; set; } = 20;
    }
}
