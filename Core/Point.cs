using System;

namespace Core
{
    public readonly struct Point : IEquatable<Point>, IComparable<Point>
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }

        public int Y { get; }

        public static Point Origin { get; } = new Point(0, 0);

        public bool Equals(Point point) => X == point.X && Y == point.Y;

        public bool NotEquals(Point point) => !Equals(point);

        public override bool Equals(object? obj) => obj is Point coordinate && Equals(coordinate);

        public bool NotEquals(object? obj) => !Equals(obj);

        public override int GetHashCode() => (X, Y).GetHashCode();

        public void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
        }

        public static bool operator ==(Point point1, Point point2) => point1.Equals(point2);

        public static bool operator !=(Point point1, Point point2) => point1.NotEquals(point2);

        public static implicit operator Point((int, int) tuple) => ToPoint(tuple);

        public static Point ToPoint((int, int) tuple) => new Point(tuple.Item1, tuple.Item2);

        public static implicit operator (int x, int y)(Point point) => ToValueTuple(point);

        public static (int x, int y) ToValueTuple(Point c) => (c.X, c.Y);

        int IComparable<Point>.CompareTo(Point other) => CompareTo(other);

        public int CompareTo(Point other) => this.Distance().CompareTo(other.Distance());

        public int CompareTo(Point other, Point reference) => this.Distance(reference).CompareTo(other.Distance(reference));

        public static bool operator <(Point point1, Point point2) => point1.CompareTo(point2) < 0;

        public static bool operator <=(Point point1, Point point2) => point1.CompareTo(point2) <= 0;

        public static bool operator >(Point point1, Point point2) => point1.CompareTo(point2) > 0;

        public static bool operator >=(Point point1, Point point2) => point1.CompareTo(point2) >= 0;

        public static implicit operator Point(Direction direction) => direction.ToPoint();
    }
}
