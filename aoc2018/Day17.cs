using System;
namespace AdventOfCode
{
    public class Day17
    {
        void PrintGrid(char[,] grid)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 300; x < grid.GetLength(0); x++)
                {
                    Console.Write(grid[x, y] == new char() ? '.' : grid[x, y]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public int HowMuchWater()
        {
            string[] input = System.IO.File.ReadAllLines(@"input/Day17.txt");

            char[,] grid = new char[520, 2054];
            foreach (String l in input)
            {
                string[] ls = l.Split(", ");
                if (ls[0].StartsWith('x'))
                {
                    int x = int.Parse(ls[0].Split('=')[1]);
                    string[] yRange = ls[1].Split('=')[1].Split('.');
                    for (int y = int.Parse(yRange[0]); y <= int.Parse(yRange[2]); y++) grid[x, y] = '#';
                }
                else
                {
                    int y = int.Parse(ls[0].Split('=')[1]);
                    string[] xRange = ls[1].Split('=')[1].Split('.');
                    for (int x = int.Parse(xRange[0]); x <= int.Parse(xRange[2]); x++) grid[x, y] = '#';
                }
            }
            grid[500, 0] = '+';
            PrintGrid(grid);

            bool somethingChanged = true;
            while (somethingChanged)
            {
                somethingChanged = false;
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    for (int x = 0; x < grid.GetLength(0); x++)
                    {
                        if (grid[x, y] == '|' || grid[x, y] == '+')
                        {
                            if (y + 1 >= grid.GetLength(1) || grid[x, y + 1] == '|' || grid[x, y + 1] == '-') continue;
                            if (grid[x, y + 1] == '\0')
                            {
                                grid[x, y + 1] = '|';
                                somethingChanged = true;
                                continue;
                            }
                            // fill
                            int l = x;
                            int r = x;
                            while (grid[l - 1, y] != '#' && grid[l, y + 1] != '\0') l--;
                            while (grid[r + 1, y] != '#' && grid[r, y + 1] != '\0') r++;
                            if (grid[l - 1, y] == '#' && grid[r + 1, y] == '#')
                            {
                                for (; l <= r; l++) grid[l, y] = '~';
                            } else {
                                for (int i = l; i <= r; i++) grid[i, y] = '-';
                                if (grid[l, y + 1] == '\0') grid[l, y] = '|';
                                if (grid[r, y + 1] == '\0') grid[r, y] = '|';
                            }
                            somethingChanged = true;
                        }
                    }
                }
            }
            PrintGrid(grid);

            int water = 0;
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 300; x < grid.GetLength(0); x++)
                {
                    if(grid[x, y] == '|' || grid[x, y] == '~' || grid[x, y] == '-') water++;
                }
            }

            return water;
        }
    }
}
