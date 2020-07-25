using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Core
{
    public class World
    {
        public World() : this(new Options())
        {
        }

        // Fix this non-dry stuff
        public World(Options options)
        {
            if (options is null)
            {
                throw new ArgumentException($"{nameof(options)} cannot be null");
            }

            Size = options.Size;

            var center = Size.GetCenter().EnsuredWithin(Size);

            Apple = new Point(center.X + 1, center.Y + 1).EnsuredWithin(Size);

            Snake = ( options.Snake ?? new List<Point>() { center } ).EnsuredWithin(Size);

            RandomSeed = options.RandomSeed ?? new Random().Next();

            rand = new Random(RandomSeed);
        }

        public int RandomSeed { get; }

        private readonly Random rand;

        public Point Size { get; }

        public Point Apple { get; private set; }

        public List<Point> Snake { get; }
        
        public WorldState Update(Direction direction)
        {
            var tailEnd = Snake.GetTailEnd();

            Snake.Move(direction);

            if (Snake.IsInvalidState(Size))
            {
                return WorldState.Invalid;
            }

            if (Snake.GetHead() == Apple)
            {
                Snake.Add(tailEnd);


                // Regenerate if apple is inside snake
                do
                {
                    // Generated apple to be somewhere near snake head, and within size

                    var head = Snake.GetHead();

                    int xOffset = Size.X / 10;
                    int x = rand.Next(head.X - xOffset, head.X + xOffset);
                    x = x.EnsuredWithin(Size.X);

                    int yOffset = Size.Y / 10;
                    int y = rand.Next(head.Y - yOffset, head.Y + yOffset);
                    y = y.EnsuredWithin(Size.Y);

                    Apple = new Point(x, y);

                } while (Snake.Contains(Apple));

            }

            return WorldState.Running;

        }
    }
}
