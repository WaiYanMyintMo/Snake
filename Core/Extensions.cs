namespace Core
{
    public static class Extensions
    {

        public static int EnsuredWithin(this int num, int lowerBoundInclusive, int higherBoundExclusive)
            => num < lowerBoundInclusive ? lowerBoundInclusive
            : num >= higherBoundExclusive ? higherBoundExclusive - 1
            : num;

        public static int EnsuredWithin(this int num, int higherBoundExclusive) => num.EnsuredWithin(0, higherBoundExclusive);

        public static int EnsuredWithin(this int num) => num.EnsuredWithin(0, num + 1);

        public static bool IsWithin(this int num, int lowerBoundInclusive, int higherBoundExclusive)
            => num >= lowerBoundInclusive && num < higherBoundExclusive;

        public static bool IsWithin(this int num, int higherBoundExclusive) => num.IsWithin(0, higherBoundExclusive);
    }
}
