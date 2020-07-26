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
        public Render(World world)
        {
            this.world = world;
            size = this.world.Size;

            sb = new StringBuilder((size.X * size.Y) + size.Y);
            buffer = GetEmptyBuffer(size);

            InitializeConsole(world);
        }

        private readonly World world;

        private readonly Point size;

        private static int windowWidthOld;

        private static int windowHeightOld;

        public static void InitializeConsole(World world)
        {
            try
            {
                Title = "Snake";
                CursorVisible = false;

                var size = world.Size;

                var width = size.X;
                var height = size.Y;

                // TODO: implement console buffer / padding / margin

                if (size >= new Point(LargestWindowWidth, LargestWindowHeight))
                {
                    Clear();
                    WriteLine($"You are running at max world size. Screen may flicker");
                    WriteLine($"  Potential solution: Try running in windowed mode instead of fullscreen");
                    WriteLine("To change world size, please consult \"--help\" command line option");
                    WriteLine("Set verbose mode to true for more info");
                    Write("Press any key to continue...");
                    ReadKey(true);
                }

                if (width > LargestWindowWidth || height > LargestWindowHeight)
                {
                    throw new Exception("World size too large to render");
                }

                windowWidthOld = WindowWidth;
                windowHeightOld = WindowHeight;


                SetWindowSize(width, height);
            } catch (PlatformNotSupportedException) { }

            Clear();
        }

        public static void CleanupConsole()
        {
            try
            {
                SetWindowSize(windowWidthOld, windowHeightOld);
            } catch (PlatformNotSupportedException) { }
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

        private void PrepareBuffer()
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

            if (buffer[head.Y][head.X] == 'S')
            {
                buffer[head.Y][head.X] = 'X';
            }
        }

        private void PrepareStringBuilderFromBuffer()
        {
            sb.Clear();

            for (int i = 0; i < size.Y; i++)
            {
                sb.Append(buffer[i]);
                if (i != size.Y - 1)
                {
                    sb.Append(Environment.NewLine);
                }
            }
        }

        public void Draw()
        {
            PrepareBuffer();
            PrepareStringBuilderFromBuffer();

            SetCursorPosition(0, 0);
            Write(sb);
        }

        public void DisplayGameEnd(WorldState worldState)
        {
            var center = world.Size.GetCenter();

            var sb = new StringBuilder();

            if (worldState is WorldState.Invalid)
            {
                sb.Append(" Game over.");
            }
            else if (worldState is WorldState.Won)
            {
                sb.Append(" Congratulations, you won.");
            }
            sb.Append($" Your score was {world.Snake.Count}.");

            try
            {
                SetWindowSize(WindowWidth, (WindowHeight + 2).EnsuredWithin(LargestWindowHeight));
            } catch (PlatformNotSupportedException) { }
            SetCursorPosition((center.X - (sb.Length / 2)).EnsuredWithin(), WindowHeight - 2);

            WriteLine(sb);

            SetCursorPosition(0, WindowHeight - 1);
        }
    }
}
