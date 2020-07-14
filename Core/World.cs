using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class World
    {
        private readonly Random rand = new Random();
        public World() : this((50, 20))
        {
        }

        public World(Point size)
        {
            Size = size;

            var center = size.GetCenter().EnsureWithin(size);

            Apple = new Point(center.X + 1, center.Y + 1).EnsureWithin(size);

            Snake = new List<Point>() { center };
        }

        public World(Point size, Point apple, List<Point> snake)
        {
            Size = size;
            Apple = apple.EnsureWithin(size);
            Snake = snake.EnsureWithin(size);
        }

        public Point Size { get; }

        public Point Apple { get; private set; }

        public List<Point> Snake { get; }
        
        public void Update(Direction direction)
        {
            var tailEnd = Snake.GetTailEnd();

            Snake.Move(direction);

            if (Snake.IsInvalidState(Size))
            {
                throw new Exception("Game over");
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

        }
    }
}
