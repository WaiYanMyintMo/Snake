using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public static class PointExtensions
    {
        public static Point ToPoint(this Direction direction) => direction switch
        {
            Direction.Up => (0, 1),
            Direction.Down => (0, -1),
            Direction.Left => (-1, 0),
            Direction.Right => (1, 0),
            _ => (0, 0)
        };

        public static Point WithX(this Point point, int x) => new Point(x, point.X);

        public static Point WithY(this Point point, int y) => new Point(point.X, y);

        public static Point Add(this Point point1, Point point2) => new Point(point1.X + point2.X, point1.Y + point2.Y);

        public static Point GetCenter(this Point point) => new Point(point.X / 2, point.Y / 2);

        public static Point EnsureWithin(this Point point, Point size)
            => point
                .WithX(point.X.EnsuredWithin(size.X))
                .WithY(point.Y.EnsuredWithin(size.Y));

        public static bool IsWithin(this Point point, Point size) 
            => point.X.IsWithin(size.X) && point.Y.IsWithin(size.Y);
    }
}
