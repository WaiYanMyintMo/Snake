using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class World
    {
        public World() : this((50, 50))
        {
        }

        public World(Coordinate size) : this(size, new Coordinate())
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

            Snake = Snake.WithMovement(direction);

            if (Snake.isCollided)
            {
                throw new Exception("Game over");
            }

        }
    }
}
