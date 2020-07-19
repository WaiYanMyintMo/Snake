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

            var render = new Render(world);

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
            double rng = rand.NextDouble();
            return rng >= 0.75 ? Up : rng >= 0.5 ? Down : rng >= 0.25 ? Left : Right;
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
