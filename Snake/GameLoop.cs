using Core;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Core.Direction;

namespace Snake
{
    public class GameLoop
    {
        public GameLoop(World world, int millisecondsPerUpdate)
        {
            Contract.Requires(!(world is null));

            this.world = world;
            this.millisecondsPerUpdate = millisecondsPerUpdate;
            render = new Render(world);
        }

        private readonly World world;

        private readonly Render render;

        private readonly int millisecondsPerUpdate;

        private Direction currentDirection;

        public void Start()
        {
            render.Draw();

            currentDirection = Input.ForceGetDirection();

            var worldState = WorldState.Running;

            while (worldState is WorldState.Running)
            {
                render.Draw();
                // Starts a task that waits for some time
                var waitTask = Task.Run(() => Task.Delay(millisecondsPerUpdate));

                // in that time, we check for input (blocking)

                // Change next direction if not timeout, or if key is valid
                var newDirection = currentDirection;
                while (!waitTask.IsCompleted)
                {
                    if (Console.KeyAvailable)
                    {
                        var nullableDirection = Input.GetDirection();
                        if (!(nullableDirection is null))
                        {
                            var direction = (Direction)nullableDirection;
                            // So that you don't turn backwards
                            if (direction.Opposite() != currentDirection)
                            {
                                newDirection = direction;
                            }
                        }
                    }
                }

                currentDirection = newDirection;
                worldState = world.Update(currentDirection);

                try
                {
                    render.Draw();
                }
                catch(IndexOutOfRangeException) { }
            }

            render.DisplayGameEnd(worldState);
        }
    }
}
