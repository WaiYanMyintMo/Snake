using Core;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Core.Direction;

namespace Snake
{
    public class GameLoop
    {
        public GameLoop(World world, double millisecondsPerUpdate)
        {
            Contract.Requires(!(world is null));

            this.world = world;
            timespanPerUpdate = TimeSpan.FromMilliseconds(millisecondsPerUpdate);
            render = new Render(world);
        }

        private readonly World world;

        private readonly Render render;

        private readonly TimeSpan timespanPerUpdate;

        private readonly Queue<Direction> currentDirectionBuffer = new Queue<Direction>();

        private Direction lastUsedDirection;

        private Direction lastQueuedDirection;

        public void Start()
        {
            render.Draw();

            Console.SetCursorPosition(0, 0);
            Console.Write("Press any input keys (WASD, ArrowKeys) to start...");

            currentDirectionBuffer.Enqueue(lastUsedDirection = DirectionInput.ForceGetDirection());

            var worldState = WorldState.Running;

            while (worldState is WorldState.Running)
            {
                Task.Run(() => render.Draw());
                // Starts a task that waits for some time

                var waitTask = Task.Run(() => Task.Delay(timespanPerUpdate));

                // in that time, we check for input (blocking)

                // Change next direction if not timeout, or if key is valid

                while (!waitTask.IsCompleted)
                {
                    if (Console.KeyAvailable)
                    {
                        var consoleKey = Console.ReadKey(true).Key;

                        if (consoleKey is ConsoleKey.C)
                        {
                            currentDirectionBuffer.Clear();
                        }
                        else
                        {
                            var nullableDirection = consoleKey.ConsoleKeyToDirection();
                            if (!(nullableDirection is null))
                            {
                                var direction = (Direction)nullableDirection;
                                // So that you don't turn backwards
                                if (direction != lastQueuedDirection && direction.Opposite() != lastQueuedDirection)
                                {
                                    currentDirectionBuffer.Enqueue(lastQueuedDirection = direction);
                                }
                            }
                        }
                    }
                }


                if (currentDirectionBuffer.Count > 0)
                {
                    lastUsedDirection = currentDirectionBuffer.Dequeue();
                }

                worldState = world.Update(lastUsedDirection);
            }

            try
            {
                render.Draw();
            }
            catch (IndexOutOfRangeException) { }

            render.DisplayGameEnd(worldState);

            Render.CleanupConsole();
        }
    }
}
