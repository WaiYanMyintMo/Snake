namespace Core
{
    public static class Extensions
    {

        public static int EnsureWithin(this int num, int lowerBoundInclusive, int higherBoundExclusive)
            => num < lowerBoundInclusive ? lowerBoundInclusive
            : num >= higherBoundExclusive ? higherBoundExclusive - 1
            : num;

        public static int EnsureWithin(this int num, int higherBoundExclusive) => num.EnsureWithin(0, higherBoundExclusive);

        public static bool IsWithin(this int num, int lowerBoundInclusive, int higherBoundExclusive)
            => num >= lowerBoundInclusive && num < higherBoundExclusive;

        public static bool IsWithin(this int num, int higherBoundExclusive) => num.IsWithin(0, higherBoundExclusive);
    }
}
