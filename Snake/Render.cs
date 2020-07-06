using Core;
using System;
using static System.Console;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Snake
{
    class Render
    {
        public Render(ref World world_)
        {
            world_ = InitializeConsole(world_);
            world = world_;
            size = world.Size;
            sb = new StringBuilder(size.X * size.Y);
        }

        private readonly World world;

        private readonly Coordinate size;

        public static World InitializeConsole(World world)
        {
            Title = "Snake";
            CursorVisible = false;

            var size = world.Size;

            var width = size.X;
            var height = size.Y;

            if (width > LargestWindowWidth || height > LargestWindowHeight)
            {
                width = LargestWindowWidth;
                height = LargestWindowHeight;

                world.Size = (width, height);
            }

            if (height > WindowHeight) 
                SetWindowSize(WindowWidth, height);
            if (width > WindowWidth) 
                SetWindowSize(width, WindowHeight);

            return world;
        }

        private char[,] EmptyCharArrayArray
        {
            get
            {
                var array = new char[size.Y, size.X];
                for (int i = 0; i < size.Y; i++)
                {
                    for (int j = 0; j < size.X; j++)
                    {
                        array[i, j] = ' ';
                    }
                }
                return array;
            }
        }

        private readonly StringBuilder sb;

        public void draw()
        {
            sb.Clear();

            var buffer = EmptyCharArrayArray;

            buffer[world.Apple.Y, world.Apple.X] = 'a';

            var snake = world.Snake;
            var head = snake.Head;

            buffer[head.Y, head.X] = 'H';

            var headToTail = snake.HeadToTail;
            var length = headToTail.Count;
            for (int i = 1; i < length; i++)
            {
                var body = headToTail[i];
                buffer[body.Y, body.X] = 'S';
            }

            for (int i = 0; i < size.Y; i++)
            {
                for (int j = 0; j < size.X; j++)
                {
                    sb.Append(buffer[i,j]);
                }
                sb.Append(Environment.NewLine);
            }

            SetCursorPosition(0, 0);
            Write(sb);
        }
    }
}
