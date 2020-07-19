using System;
using static Core.Direction;

namespace Core
{
    public static class DirectionExtensions
    {
        public static Direction Opposite(this Direction direction) => direction switch
        {
            Up => Down,
            Down => Up,
            Left => Right,
            Right => Left,
            _ => throw new ArgumentException($"{nameof(direction)} arg's value {direction} is invalid")
        };
    }
}
