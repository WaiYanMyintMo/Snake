using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class World
    {
        private readonly Random rand = new Random();
        public World() : this((50, 50))
        {
        }

        public World(Coordinate size) : this(size, new Coordinate((size.X / 2) + 1, (size.Y / 2) + 1))
        {
        }

        public World(Coordinate size, Coordinate apple) : this(size, apple, Snake.FromWorldSize(size))
        {
        }

        public World(Coordinate size, Coordinate apple, Snake snake)
        {
            Size = size;
            Apple = apple;
            Snake = snake;
        }

        public Coordinate Size { get; }

        public Coordinate Apple { get; set; }

        public Snake Snake { get; set; }
        
        public void Update(Direction direction)
        {
            var tail = Snake.Tail;

            Snake = Snake.WithMovement(direction);

            if (Snake.isCollided(Size))
            {
                throw new Exception("Game over");
            }

            if (Snake.Head == Apple)
            {
                var newHeadToTail = Snake.HeadToTail.ToList();
                newHeadToTail.Add(tail);
                Snake = new Snake(newHeadToTail);

                int index = (int)(Size.X * Size.Y * rand.NextDouble());
                var y = Math.DivRem(index, Size.Y, out int x);

                Apple = new Coordinate(x, y);

            }

        }
    }
}
