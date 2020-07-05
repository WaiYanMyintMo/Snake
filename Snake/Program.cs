using System;
using Core;
using static Core.Direction;

namespace Snake
{
    class Program
    {
        static void Main()
        {
            var world = new World();

            var rand = new Random(123);

            for (int i = 0; i >= 0; i++)
            {
                double rng = rand.NextDouble();
                Direction direction = rng >= 0.75 ? Up : rng >= 0.5 ? Down : rng >= 0.25 ? Left : Right;

                world.Update(direction);

                if (i % 100 == 0)
                {

                }
            }

            { }
        }
    }
}
