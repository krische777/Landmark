using System;
using System.IO;
using System.Xml.Linq;

namespace CS_draw_grid_prob
{
    class Program
    {
        public static int minesHitCounter = 0;

        static void Main(string[] args)
        {
            StartGame();
        }
        static void StartGame()
        {
            char[,] grid = new char[8, 8];

            InitializeEmptyGrid(grid);
            GenerateMines(grid);
            SetInitialPosition(grid);
            ShowGrid(grid);
            bool isPreviousMine = false;

            Console.WriteLine(" ");
            Console.WriteLine("Press keys to change direction:");

            while (true)
            {
                int nextX = -1;
                int nextY = -1;
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        nextX = CurrentPosition.positionX;
                        nextY = CurrentPosition.positionY - 1;
                        break;
                    case ConsoleKey.LeftArrow:
                        nextX = CurrentPosition.positionX - 1;
                        nextY = CurrentPosition.positionY;
                        break;
                    case ConsoleKey.DownArrow:
                        nextX = CurrentPosition.positionX;
                        nextY = CurrentPosition.positionY + 1;
                        break;
                    case ConsoleKey.RightArrow:
                        nextX = CurrentPosition.positionX + 1;
                        nextY = CurrentPosition.positionY;
                        break;
                }

                if (CheckGridBounds(grid, nextX, nextY))
                {
                    Console.WriteLine("Choose another direction");
                    continue;
                };

                if (isPreviousMine)
                {
                    grid[CurrentPosition.positionY, CurrentPosition.positionX] = '@';
                }
                else
                {
                    grid[CurrentPosition.positionY, CurrentPosition.positionX] = ' ';
                }
                if (grid[nextY, nextX] == 'X')
                {
                    isPreviousMine = true;
                    minesHitCounter++;
                }
                else
                {
                    isPreviousMine = false;
                }
                CurrentPosition.positionY = nextY;
                CurrentPosition.positionX = nextX;
                grid[CurrentPosition.positionY, CurrentPosition.positionX] = '#';
                Console.Clear();
                ShowGrid(grid);
                if (minesHitCounter >= 3)
                {
                    Console.WriteLine("");
                    Console.WriteLine("You lost!");
                    minesHitCounter = 0;
                    break;
                }
                else if (CurrentPosition.positionX == grid.GetLength(0) - 1)
                {
                    Console.WriteLine("");
                    Console.WriteLine("You won!");
                    minesHitCounter = 0;
                    break;
                }
            }
            StartGame();
        }

        static void InitializeEmptyGrid(char[,] grid)
        {            
            for (int r = 0; r < grid.GetLength(0); ++r)
            {
                for (int c = 0; c < grid.GetLength(1); ++c)
                {
                    grid[r, c] = ' ';
                }
            }
        }
        static void ShowGrid(char[,] grid)
        {
            Console.Write(" ");
            for (int r = 1; r <= grid.GetLength(0); ++r)
            {
                Console.Write("{0} ", r);
            }
            Console.WriteLine();
            for (int r = 0; r < grid.GetLength(0); ++r)
            {
                Console.Write("{0}", r + 1);
                for (int c = 0; c < grid.GetLength(1); ++c)
                {
                    if(grid[r, c] != 'X')
                    {
                        Console.Write("{0} ", grid[r, c]);
                    }else
                    {
                        Console.Write("{0} ", ' ');
                    }
                }
                Console.WriteLine();
            }
        }

        static void GenerateMines(char[,] grid)
        {
            Random rnd = new Random();
            for (int x = 0; x < grid.GetLength(0)+ grid.GetLength(1); ++x)
            {
                int mineY = rnd.Next(0, grid.GetLength(0));
                int mineX = rnd.Next(0, grid.GetLength(1));
                grid[mineY, mineX] = 'X';
            }
        }

        static void SetInitialPosition(char[,] grid)
        {
            Random rnd = new Random();
            CurrentPosition.positionY = rnd.Next(0, grid.GetLength(1));
            CurrentPosition.positionX = 0;

            grid[CurrentPosition.positionY, CurrentPosition.positionX] = '#';
        }

        static bool CheckGridBounds(char[,] grid, int nextX, int nextY)
        {
            return nextY < 0 || nextX < 0 || nextY > grid.GetLength(1) - 1;
        }
    }

    static class CurrentPosition
    {
        public static int positionX;
        public static int positionY;
    }
}