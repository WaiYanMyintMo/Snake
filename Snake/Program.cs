using System;
using Core;

namespace Snake
{
    class Program
    {
        static void Main(params string[] args)
        {
            if (args.Length == 1)
            {
                Game.Run(int.Parse(args[0]));
            }
            else
            {
                Game.Run(250);
            }
        }

    }
}
