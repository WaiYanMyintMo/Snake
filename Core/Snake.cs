﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Core
{
    public readonly struct Snake : IEquatable<Snake>
    {
        public Snake(IEnumerable<Coordinate> headToTail)
        {
            var headToTailArray = headToTail.ToArray();

            if (headToTailArray.Length < 1)
            {
                throw new FormatException($"{nameof(headToTail)} must be at least 1");
            }

            HeadToTail = new ReadOnlyCollection<Coordinate>(headToTail.ToList());
        }

        public static Snake FromWorldSize(Coordinate size) => new Snake(new Coordinate[] { (size.X / 2, size.Y / 2) });

        public static Snake Default => new Snake(new Coordinate[] { (0, 0) });

        public ReadOnlyCollection<Coordinate> HeadToTail { get; }

        public Coordinate Head => HeadToTail[0];

        public bool Equals(Snake other) => HeadToTail == other.HeadToTail;

        public override bool Equals(object obj) => obj is Snake snake && Equals(snake);

        public override int GetHashCode() => HeadToTail.GetHashCode();

        public static bool operator ==(Snake one, Snake other) => one.Equals(other);

        public static bool operator !=(Snake one, Snake other) => !one.Equals(other);

        public static implicit operator Snake(Coordinate[] coordinates) => ToSnake(coordinates);

        public static Snake ToSnake(IEnumerable<Coordinate> coordinates) => new Snake(coordinates);

        public static implicit operator Coordinate[](Snake snake) => FromSnake(snake).ToArray();

        public static IEnumerable<Coordinate> FromSnake(Snake snake) => snake.HeadToTail;

        public Snake WithMovement(Direction direction) => WithMovement(Coordinate.ToCoordinate(direction));

        public Snake WithMovement(Coordinate vector)
        {
            var headToTail = HeadToTail;
            var snakeLength = headToTail.Count;

            var newHeadToTail = new Coordinate[snakeLength];
            newHeadToTail[0] = (Head.X + vector.X, Head.Y + vector.Y);

            for (int i = 1; i < snakeLength; i++)
            {
                newHeadToTail[i] = headToTail[i - 1];
            }

            return new Snake(newHeadToTail);
        }

        public bool isCollided(Coordinate size)
        {
            foreach (var block in HeadToTail)
            {
                if (block.X > size.X || block.Y > size.Y)
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
                var hashset = new HashSet<Coordinate>();
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