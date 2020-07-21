using System;

namespace Core
{
    public readonly struct Point : IEquatable<Point>
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }

        public int Y { get; }
        
        public bool Equals(Point point) => X == point.X && Y == point.Y;

        public bool NotEquals(Point point) => !Equals(point);

        public override bool Equals(object obj) => obj is Point coordinate && Equals(coordinate);

        public bool NotEquals(object obj) => !Equals(obj);

        public override int GetHashCode() => (X, Y).GetHashCode();

        public void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
        }

        public static bool operator ==(Point one, Point other) => one.Equals(other);

        public static bool operator !=(Point one, Point other) => one.NotEquals(other);

        public static implicit operator Point((int, int) tuple) => ToPoint(tuple);

        public static Point ToPoint((int, int) tuple) => new Point(tuple.Item1, tuple.Item2);

        public static implicit operator (int x, int y)(Point point) => ToValueTuple(point);

        public static (int x, int y) ToValueTuple(Point c) => (c.X, c.Y);

        public static implicit operator Point(Direction direction) => direction.ToPoint();
    }
}
