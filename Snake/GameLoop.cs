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

            var worldState = WorldState.Running;

            while (worldState is WorldState.Running)
            {
                render.Draw();
                // Starts a task that waits for some time and then run
                var updateAndDrawTask = UpdateAndDrawAsync();

                // in that time, we check for input (blocking)

                // Change next direction if not timeout, or if key is valid
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

                worldState = updateAndDrawTask.Result;
            }
            
            var center = world.Size.GetCenter();

            var sb = new StringBuilder();

            if (worldState is WorldState.Invalid)
            {
                sb.Append(" Game over. ");
            }
            else if (worldState is WorldState.Won)
            {
                sb.Append(" Congratulations. You won the game. ");
            }
            sb.Append($" Your score was {world.Snake.Count}. ");

            Console.SetCursorPosition((center.X - (sb.Length / 2)).EnsuredWithin(), center.Y);
            Console.Write(sb);
        }

        private async Task<WorldState> UpdateAndDrawAsync()
        {
            await Task.Delay(MILLISECOND_PER_UPDATE).ConfigureAwait(false);
            return UpdateAndDraw();
        }

        private WorldState UpdateAndDraw()
        {
            return world.Update(currentDirection);
        }
    }
}
