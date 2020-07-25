using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Core
{
    public static class SnakeExtensions
    {
        public static List<Point> EnsuredWithin(this List<Point> points, Point size)
        {
            Contract.Requires(!(points is null));
            
            for (int i = 0; i < points!.Count; i++)
            {
                points[i] = points[i].EnsuredWithin(size);
            }
            return points;
        }

        public static bool IsWithin(this List<Point> points, Point size)
        {
            Contract.Requires(!(points is null));

            foreach (var point in points!)
            {
                if (!point.IsWithin(size))
                {
                    return false;
                }
            }

            return true;
        }

        public static Point GetHead(this List<Point> points) => points.First();

        public static Point GetTailEnd(this List<Point> points) => points.Last();

        public static List<Point> WithMovement(this List<Point> points, Point vector)
        {
            // first move the head, and the rest of the body

            Contract.Requires(!(points is null));

            var movedHead = points!.GetHead().Add(vector);
            var movedBodyToTailEnd = points!.GetRange(0, points.Count - 1);

            var newPoints = new List<Point>() { movedHead };
            newPoints.AddRange(movedBodyToTailEnd);

            return newPoints;
        }

        public static List<Point> WithMovement(this List<Point> points, Direction direction) => points.WithMovement(direction.ToPoint());

        public static void Move(this List<Point> points, Point vector)
        {
            Contract.Requires(!(points is null));

            var newPoints = points!.WithMovement(vector);

            points!.Clear();
            points.AddRange(newPoints);
        }

        public static void Move(this List<Point> points, Direction direction) => points.Move(direction.ToPoint());

        public static bool IsInvalidState(this List<Point> points, Point size) 
            => !points.IsWithin(size) || points.HasDuplicate();

        public static bool HasDuplicate(this List<Point> points)
        {
            Contract.Requires(!(points is null));

            // https://stackoverflow.com/a/14363597

            var hashset = new HashSet<Point>();
            foreach (var block in points!)
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
