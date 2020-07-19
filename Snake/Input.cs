using Core;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using static System.ConsoleKey;
using static Core.Direction;
using System.Threading.Tasks;

namespace Snake
{
    public static class Input
    {
        public static Direction? ConsoleKeyToDirection(this ConsoleKey consoleKey) => consoleKey switch
        {
            var x when x == UpArrow || x == W => Up,
            var x when x == LeftArrow || x == A => Left,
            var x when x == DownArrow || x == S => Down,
            var x when x == RightArrow || x == D => Right,
            _ => null,
        };

        public static Direction? GetDirection() => ReadKey(false).Key.ConsoleKeyToDirection();

        public static Direction ForceGetDirection()
        {
            Direction? inputDirection = null;
            while (inputDirection is null)
            {
                inputDirection = GetDirection();
            }

            return (Direction)inputDirection;
        }

        public static async Task<Direction?> GetDirectionAsync()
        {
            var task = Task.Run(() => GetDirection());
            return await task;
            /*
            if (task.Wait(TimeSpan.FromMilliseconds(MILLISECOND_PER_UPDATE)) && !(task.Result is null))
            {
                NextDirection = (Direction)task.Result;
            }
            else
            {
                task.
                }
            */
        }
    }
}
