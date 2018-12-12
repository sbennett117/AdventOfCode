using System;
namespace AdventOfCode
{
    public class Day11
    {
        int input = 9798;

        public void MostPowerfulThreeByThree()
        {
            int[,] grid = GenerateGrid();

            Tuple<int, int> bestCentre = new Tuple<int, int>(-1, -1);
            int highestPower = int.MinValue;
            for (int x = 0; x < grid.GetLength(0) - 2; x++)
            {
                for (int y = 0; y < grid.GetLength(1) - 2; y++)
                {
                    int power = SumSquare(grid, x, y, 3);
                    if (power > highestPower)
                    {
                        highestPower = power;
                        bestCentre = new Tuple<int, int>(x+1, y+1);
                    }
                }
            }

            Console.WriteLine(bestCentre.Item1 + "," + bestCentre.Item2);
        }

        public void MostPowerfulSquare()
        {
            int[,] grid = GenerateGrid();

            Tuple<int, int, int> bestSquare = new Tuple<int, int, int>(-1, -1, -1);
            int highestPower = int.MinValue;
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    for (int w = 0; (w + x) < grid.GetLength(0) && (w + y) < grid.GetLength(1); w++)
                    {
                        int power = SumSquare(grid, x, y, w + 1);
                        if (power > highestPower)
                        {
                            highestPower = power;
                            bestSquare = new Tuple<int, int, int>(x+1, y+1, w+1);
                        }
                    }
                }
            }

            Console.WriteLine(String.Format("{0},{1},{2}", bestSquare.Item1, bestSquare.Item2, bestSquare.Item3));
        }

        int[,] GenerateGrid()
        {
            int[,] grid = new int[300, 300];

            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++) grid[x, y] = CalculatePower(x+1, y+1);
            }

            return grid;
        }

        int CalculatePower(int x, int y)
        {
            int rackID = x + 10;
            int power = (rackID * y + input) * rackID;
            int thirdDigit = power > 99 ? (power / 100) % 10 : 0;
            return thirdDigit - 5;
        }

        int SumSquare(int[,] grid, int tlx, int tly, int width)
        {
            int sum = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++) sum += grid[tlx + x, tly + y];
            }

            return sum;
        }
    }
}
