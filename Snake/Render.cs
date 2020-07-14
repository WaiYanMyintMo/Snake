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
            buffer = GetEmptyBuffer(size);
        }

        private readonly World world;

        private readonly Point size;

        public static World InitializeConsole(World world)
        {
            Title = "Snake";
            CursorVisible = false;

            var size = world.Size;

            var width = size.X;
            var height = size.Y;

            if (width > LargestWindowWidth || height > LargestWindowHeight)
            {
                throw new Exception("World size too large to render");
            }

            if (height > WindowHeight) 
                SetWindowSize(WindowWidth, height);
            if (width > WindowWidth) 
                SetWindowSize(width, WindowHeight);

            return world;
        }

        private static char[][] GetEmptyBuffer(Point size)
        {
            var buffer = new char[size.Y][];
            EmptyBuffer(buffer, size.X);
            return buffer;
        }

        private static void EmptyBuffer(char[][] buffer, int xLength)
        {
            for (int y = 0; y < buffer.Length; y++)
            {
                buffer[y] = new char[xLength];
                for (int x = 0; x < xLength; x++)
                {
                    buffer[y][x] = ' ';
                }
            }
        }

        private static void EmptyBuffer(char[][] buffer)
        {
            if (buffer.Length > 0)
            {
                EmptyBuffer(buffer, buffer[0].Length);
            }
        }

        private readonly StringBuilder sb;

        private readonly char[][] buffer;

        private char[][] PrepareBuffer()
        {
            EmptyBuffer(buffer);

            var apple = world.Apple;
            buffer[apple.Y][apple.X] = 'a';

            var snake = world.Snake;

            var head = snake.GetHead();

            buffer[head.Y][head.X] = 'H';

            var bodyToTailEnd = snake.GetRange(1, snake.Count - 1);

            foreach (var body in bodyToTailEnd)
            {
                buffer[body.Y][body.X] = 'S';
            }

            return buffer;
        }

        public void draw()
        {
            sb.Clear();

            var buffer = prepareBuffer();

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
