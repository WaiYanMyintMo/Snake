using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Core
{
    public static class PointExtensions
    {
        // remove in C# 9 use with expression instead

        public static Point WithX(this Point point, int x) => new Point(x, point.X);

        public static Point WithY(this Point point, int y) => new Point(point.X, y);

        public static Point Add(this Point point1, Point point2) => new Point(point1.X + point2.X, point1.Y + point2.Y);

        public static Point Difference(this Point point1, Point point2) => Add(point1, point2.Negative());

        public static Point Both(this Point point, Func<int, int> func)
        {
            Contract.Assert(!(func is null));
            return (func.Invoke(point.X), func.Invoke(point.Y));
        }

        public static Point Negative(this Point point) => point.Both((x) => -x);

        public static int Distance(this Point point1, Point point2)
        {
            var difference = point1.Difference(point2);
            return Math.Abs(difference.X) + Math.Abs(difference.Y);
        }

        public static int Distance(this Point point) => point.Distance(Point.Origin);

        public static Point GetCenter(this Point point) => point.Both((x) => x / 2);

        public static Point EnsuredWithin(this Point point, Point size)
            => new Point(point.X.EnsuredWithin(size.X), point.Y.EnsuredWithin(size.Y));

        public static bool IsWithin(this Point point, Point size) 
            => point.X.IsWithin(size.X) && point.Y.IsWithin(size.Y);

        public static Point ToPoint(this Direction direction) => direction switch
        {
            Direction.Left => (-1, 0),
            Direction.Up => (0, -1),
            Direction.Down => (0, 1),
            Direction.Right => (1, 0),
            _ => throw new ArgumentException($"{nameof(direction)} arg's value {direction} is invalid"),
        };

        public static IEnumerable<Point> Range(this Point exclusiveUpperBound) => Range(Point.Origin, exclusiveUpperBound);

        public static IEnumerable<Point> Range(this Point inclusiveLowerBound, Point exclusiveUpperBound)
        {
            var diff = exclusiveUpperBound.Difference(inclusiveLowerBound);
            if (diff.X <= 0 || diff.Y <= 0)
            {
                throw new ArgumentException($"{nameof(exclusiveUpperBound)} cannot be smaller than {nameof(inclusiveLowerBound)}");
            }
            for (int y = inclusiveLowerBound.Y; y < exclusiveUpperBound.Y; y++)
            {
                for (int x = inclusiveLowerBound.X; x < exclusiveUpperBound.X; x++)
                {
                    yield return (x, y);
                }
            }
        }
    }
}
