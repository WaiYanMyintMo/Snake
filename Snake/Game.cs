using Core;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using System.Threading;
using static Core.Direction;

namespace Snake
{
    public static class Game
    {
        public static void Run()
        {
            var world = new World();
            var gameLoop = new GameLoop(world);
            gameLoop.Start();
        }

        public static void RunRandom()
        {
            var world = new World();

            var render = new Render(world);

            var rand = new Random(15205);
            while (true)
            {
                world.RandomUpdate(rand);
                render.Draw();
                Thread.Sleep(50);
            }
        }

        public static Direction RandomDirection(Random rand)
        {
            Contract.Requires(!(rand is null));
            return rand.NextDouble() switch
            {
                // replace with relational patterns
                var x when x >= 0.75 => Left,
                var x when x >= 0.50 => Up,
                var x when x >= 0.25 => Down,
                var x when x >= 0.00 => Right,
                var x => throw new Exception($"{nameof(rand.NextDouble)}'s value {x} should be >= 0.0 and < 1.0"),
            };
        }

        public static void RandomUpdate(this World world, Random rand)
        {
            Contract.Requires(!(world is null));
            world.Update(RandomDirection(rand));
        }
        public static void RandomUpdate(this World world, Random rand, Action afterUpdate)
        {
            Contract.Requires(!(afterUpdate is null));
            RandomUpdate(world, rand);
            afterUpdate();
        }
    }
}
