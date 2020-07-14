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
        
        public bool Equals(Point other) => X == other.X && Y == other.Y;

        public override bool Equals(object obj) => obj is Point coordinate && Equals(coordinate);

        public override int GetHashCode() => (X, Y).GetHashCode();

        public static bool operator ==(Point one, Point other) => one.Equals(other);

        public static bool operator !=(Point one, Point other) => !one.Equals(other);

        public static implicit operator Point(ValueTuple<int, int> tuple) => ToPoint(tuple);

        public static Point ToPoint(ValueTuple<int, int> tuple) => new Point(tuple.Item1, tuple.Item2);

        public static implicit operator ValueTuple<int, int>(Point c) => ToValueTuple(c);

        public static ValueTuple<int, int> ToValueTuple(Point c) => (c.X, c.Y);

        public static Point ToCoordinate(Direction direction)
        {
            return direction switch
            {
                Direction.Up => (0, 1),
                Direction.Down => (0, -1),
                Direction.Left => (-1, 0),
                Direction.Right => (1, 0),
                _ => (0, 0)
            };
        }
    }
}
