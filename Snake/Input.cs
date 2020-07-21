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
            // C# 9, use "or" pattern matching
            var x when x is LeftArrow || x is A => Left,
            var x when x is UpArrow || x is W => Up,
            var x when x is DownArrow || x is S => Down,
            var x when x is RightArrow || x is D => Right,
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
