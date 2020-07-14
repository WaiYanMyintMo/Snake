using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Core
{
    public readonly struct Snake : IEquatable<Snake>
    {
        public Snake(IEnumerable<Point> headToTail)
        {
            var headToTailArray = headToTail.ToArray();

            if (headToTailArray.Length < 1)
            {
                throw new FormatException($"{nameof(headToTail)} must be at least 1");
            }

            HeadToTail = new ReadOnlyCollection<Point>(headToTail.ToList());
        }

        public static Snake FromWorldSize(Point size) => new Snake(new Point[] { (size.X / 2, size.Y / 2) });

        public static Snake Default => new Snake(new Point[] { (0, 0) });

        public ReadOnlyCollection<Point> HeadToTail { get; }

        public Point Head => HeadToTail[0];

        public Point Tail => HeadToTail.Last();

        public bool Equals(Snake other) => HeadToTail == other.HeadToTail;

        public override bool Equals(object obj) => obj is Snake snake && Equals(snake);

        public override int GetHashCode() => HeadToTail.GetHashCode();

        public static bool operator ==(Snake one, Snake other) => one.Equals(other);

        public static bool operator !=(Snake one, Snake other) => !one.Equals(other);

        public static implicit operator Snake(Point[] coordinates) => ToSnake(coordinates);

        public static Snake ToSnake(IEnumerable<Point> coordinates) => new Snake(coordinates);

        public static implicit operator Point[](Snake snake) => FromSnake(snake).ToArray();

        public static IEnumerable<Point> FromSnake(Snake snake) => snake.HeadToTail;

        public Snake WithMovement(Direction direction) => WithMovement(Point.ToCoordinate(direction));

        public Snake WithMovement(Point vector)
        {
            var headToTail = HeadToTail;
            var snakeLength = headToTail.Count;

            var newHeadToTail = new Point[snakeLength];
            newHeadToTail[0] = (Head.X + vector.X, Head.Y + vector.Y);

            for (int i = 1; i < snakeLength; i++)
            {
                newHeadToTail[i] = headToTail[i - 1];
            }

            return new Snake(newHeadToTail);
        }

        public bool isCollided(Point size)
        {
            foreach (var block in HeadToTail)
            {
                if (block.X >= size.X || block.X < 0 || block.Y >= size.Y || block.Y < 0)
                {
                    return true;
                }
            }
            return hasDuplicate;
        }

        public bool hasDuplicate
        {
            get
            {
                var hashset = new HashSet<Point>();
                foreach (var block in HeadToTail)
                {
                    if (!hashset.Add(block))
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
