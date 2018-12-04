using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day03
    {
        public int[] ParseClaim(string claim)
        {
            string[] parts = claim.Split(' ');
            string[] dimStr = parts[3].Split('x');
            int[] dimInt = { int.Parse(dimStr[0]), int.Parse(dimStr[1]) };

            string[] posStr = parts[2].Split(',');
            int[] posInt = { int.Parse(posStr[0]), int.Parse(posStr[1].Substring(0, posStr[1].Length - 1)) };

            int id = int.Parse(parts[0].Substring(1));

            return posInt.Concat(dimInt).Concat(new int[]{id}).ToArray();
        }

        public int[,] GenerateGrid(string[] lines)
        {
            int[,] grid = new int[1000, 1000];
            foreach (string line in lines)
            {
                int[] claim = ParseClaim(line);
                for (int i = claim[0]; i < claim[0] + claim[2]; i++)
                {
                    for (int j = claim[1]; j < claim[1] + claim[3]; j++)
                    {
                        if (grid[i,j] == 0)
                        {
                            grid[i, j] = claim[4];
                        }
                        else if (grid[i,j] != -1)
                        {
                            grid[i, j] = -1;
                        }
                    }
                }
            }
            return grid;
        }

        public int OverlapInches()
        {
            int overlap = 0;
            string[] lines = System.IO.File.ReadAllLines(@"Day03.txt");
            int[,] grid = GenerateGrid(lines);
            foreach(int i in grid)
            {
                if (i == -1)
                {
                    overlap++;
                }
            }

            return overlap;
        }

        public int ValidClaim()
        {
            string[] lines = System.IO.File.ReadAllLines(@"Day03.txt");
            int[,] grid = GenerateGrid(lines);
            foreach (string line in lines)
            {
                int[] claim = ParseClaim(line);
                bool claimOverlapped = false;
                for (int i = claim[0]; i < claim[0] + claim[2] && !claimOverlapped; i++)
                {
                    for (int j = claim[1]; j < claim[1] + claim[3] && !claimOverlapped; j++)
                    {
                        if (grid[i, j] == -1)
                        {
                            claimOverlapped = true;
                        }
                    }
                }
                if (!claimOverlapped)
                {
                    return claim[4];
                }
            }

            return 0;
        }
    }
}
