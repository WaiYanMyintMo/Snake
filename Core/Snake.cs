using System;
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
            HeadToTail = new ReadOnlyCollection<Coordinate>(headToTail.ToList());
        }

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

    }
}
