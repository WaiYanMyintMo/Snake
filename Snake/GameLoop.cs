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
        public GameLoop(World world)
        {
            Contract.Requires(!(world is null));

            this.world = world;
            render = new Render(world);
        }

        private readonly World world;

        private readonly Render render;

        private Direction currentDirection;

        const int MILLISECOND_PER_UPDATE = 250;

        public void Start()
        {
            currentDirection = Input.ForceGetDirection();

            while (true)
            {
                // Starts a task that waits for some time and then run
                var updateAndDrawTask = UpdateAndDrawAsync();

                // in that time, we check for input (blocking)

                while (!updateAndDrawTask.IsCompleted)
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
                                currentDirection = direction;
                            }
                        }
                    }
                }

                // Change next direction if not timeout, or if key is valid

            }
        }

        private async Task UpdateAndDrawAsync()
        {
            await Task.Delay(MILLISECOND_PER_UPDATE).ConfigureAwait(false);
            UpdateAndDraw();
        }

        private void UpdateAndDraw()
        {
            world.Update(currentDirection);
            render.Draw();
        }
    }
}
