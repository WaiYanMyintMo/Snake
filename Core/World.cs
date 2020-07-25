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

            var head = Snake.GetHead();
            // Grow if apple is inside snake and create another apple
            if (head == Apple)
            {
                Snake.Add(tailEnd);

                // memory vs processing speed? the classic optimization decision.

                // from a range containing all possible values in grid without the ones by snake.
                var notSnake = Size.Range().Where((p) => !Snake.Contains(p)).ToList();

                if (notSnake.Count <= 0)
                {
                    return WorldState.Won;
                }
                else
                {
                    // try to generate apple to be near snake head, or the game will be boring

                    // get inclusive lower bound and inclusive upper bound of coordinates close to snake head, within size
                    int xOffset = Size.X / 10;
                    int x0 = (head.X - xOffset).EnsuredWithin();
                    int x1 = (head.X + xOffset).EnsuredWithin(Size.X);

                    int yOffset = Size.Y / 10;
                    int y0 = (head.Y - xOffset).EnsuredWithin();
                    int y1 = (head.Y + xOffset).EnsuredWithin(Size.Y);

                    var nearHead = notSnake.Where((p) => p.X >= x0 && p.X <= x1 && p.Y >= y0 && p.Y <= y1).ToArray();
                    if (nearHead.Length > 0)
                    {
                        Apple = nearHead[rand.Next(nearHead.Length)];
                    }
                    else if (notSnake.Count == 1)
                    {
                        Apple = notSnake[0];
                    }
                    else
                    {
                        // pick number closer to snake head more likely

                        // sort closest to snake head first
                        notSnake.Sort((point1, point2) => point1.CompareTo(point2, head));

                        // https://stackoverflow.com/a/9956791/12347502 reference
                        // https://github.com/Stiles-X/WeightedRandomSelector switch to this if things get messy

                        // we want [0,1]

                        // [2, 1]
                        var discreteChunks = Enumerable.Range(1, notSnake.Count + 1).Reverse().ToArray();

                        // [2, 3]
                        var distribution = new int[discreteChunks.Length];
                        {
                            int current = distribution[0] = discreteChunks[0];
                            for (int i = 1; i < distribution.Length; i++)
                            {
                                distribution[i] = current += discreteChunks[i];
                            }
                        }

                        // linear search
                        int max = distribution[^1];
                        int randInt = (int)Math.Ceiling(rand.NextDouble() * max);
                        int foundIndex = 0;

                        while (foundIndex < distribution.Length && distribution[foundIndex] < randInt)
                        {
                            foundIndex += 1;
                        }

                        Apple = notSnake[foundIndex];

                    }
                }
            }

            return WorldState.Running;

        }
    }
}
