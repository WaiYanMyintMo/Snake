using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class World
    {
        private readonly Random rand = new Random();
        public World() : this((50, 20))
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

        public Coordinate Size { get; set; }

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

                var head = Snake.Head;

                int xOffset = Size.X / 10;
                int x = rand.Next(head.X - xOffset, head.X + xOffset);

                int yOffset = Size.Y / 10;
                int y = rand.Next(head.Y - yOffset, head.Y + yOffset);

                if (x < 0) x = 0;
                if (x >= Size.X) x = Size.X;
                if (y < 0) y = 0;
                if (y >= Size.Y) y = Size.Y;

                Apple = new Coordinate(x, y);

            }

        }
    }
}
