using System;
using System.Collections.Generic;
namespace AdventOfCode
{
    public class Day13
    {
        public string FirstCrash()
        {
            string[] input = System.IO.File.ReadAllLines(@"input/Day13.txt");
            char[,] grid = new char[input[0].Length, input.Length];
            // add cart sortedlist (and make sure the carts can sort themselves!
            List<Cart> carts = new List<Cart>();

            for (int a = 0; a < input.Length; a++)
            {
                char[] line = input[a].ToCharArray();
                for (int b = 0; b < input[0].Length; b++)
                {
                    grid[b, a] = line[b];
                    if (Cart.directions.IndexOf(line[b]) != -1) carts.Add(new Cart(b, a, line[b]));
                }
            }

            // until a crash happens
            for (int a = 0; a < grid.Length; a++)
            {
                // they're comparable, so they should sort themselves how we want them
                carts.Sort();
                foreach (Cart c in carts)
                {
                    // replace old location with currentTile
                    grid[c.X, c.Y] = c.CurrentTile;
                    // get new location from cart
                    int[] loc = c.TakeStep();
                    // if cart in new location: return here
                    if (Cart.directions.IndexOf(grid[loc[0], loc[1]]) != -1) return loc[0] + "," + loc[1];
                    // update currentTile
                    c.CurrentTile = grid[loc[0], loc[1]];
                    // add cart to new location
                    grid[loc[0], loc[1]] = c.CurrentDirection;
                }

                //for (int y = 0; y < grid.GetLength(1); y++)
                //{
                //    for (int x = 0; x < grid.GetLength(0); x++)
                //    {
                //        Console.Write(grid[x, y]);
                //    }
                //    Console.WriteLine();
                //}
            }
            return "";
        }

        public string LastCartStanding()
        {
            string[] input = System.IO.File.ReadAllLines(@"input/Day13.txt");
            char[,] grid = new char[input[0].Length, input.Length];
            List<Cart> carts = new List<Cart>();

            for (int a = 0; a < input.Length; a++)
            {
                char[] line = input[a].ToCharArray();
                for (int b = 0; b < input[0].Length; b++)
                {
                    grid[b, a] = line[b];
                    if (Cart.directions.IndexOf(line[b]) != -1) carts.Add(new Cart(b, a, line[b]));
                }
            }

            for (int a = 0; a < grid.Length; a++)
            {
                carts.Sort();
                for (int c = 0; c < carts.Count;)
                {
                    grid[carts[c].X, carts[c].Y] = carts[c].CurrentTile;
                    int[] loc = carts[c].TakeStep();
                    if (Cart.directions.IndexOf(grid[loc[0], loc[1]]) != -1)
                    {
                        carts.RemoveAt(c);
                        for (int c2 = 0; c2 < carts.Count; c2++)
                        {
                            if (carts[c2].X == loc[0] && carts[c2].Y == loc[1])
                            {
                                grid[loc[0], loc[1]] = carts[c2].CurrentTile;
                                carts.RemoveAt(c2);
                                if (c2 < c) c--;
                            }
                        }
                    } else {
                        carts[c].CurrentTile = grid[loc[0], loc[1]];
                        grid[loc[0], loc[1]] = carts[c].CurrentDirection;
                        c++;
                    }
                }

                if (carts.Count == 1) return carts[0].X + "," + carts[0].Y;
            }
            return "";
        }

        class Cart : IComparable
        {
            int[] turns = { 3, 0, 1 };
            int nextTurn;

            public static string directions = "^>v<";
            public static char[] directionsL = { '^', '>', 'v', '<' };
            public int X { get; set; }
            public int Y { get; set; }
            public char CurrentDirection { get; set; }
            public char CurrentTile { get; set; }

            public Cart(int x, int y, char direction)
            {
                X = x;
                Y = y;
                CurrentDirection = direction;
                switch (direction)
                {
                    case ('^'):
                        CurrentTile = '|';
                        break;
                    case ('v'):
                        CurrentTile = '|';
                        break;
                    case ('>'):
                        CurrentTile = '-';
                        break;
                    case ('<'):
                        CurrentTile = '-';
                        break;
                }
                nextTurn = 0;   // starts at left, then straight, then right
            }

            public int[] TakeStep(){
                int newDirection = directions.IndexOf(CurrentDirection);
                switch (CurrentTile)
                {
                    case ('\\'):
                        // Change direction to the next one in the array
                        if (newDirection % 2 == 0) newDirection = (newDirection + 3) % 4;
                        else newDirection = (newDirection + 1) % 4;
                        break;
                    case ('/'):
                        // Change direction to the previous one in the array
                        if (newDirection % 2 == 0) newDirection = (newDirection + 1) % 4;
                        else newDirection = (newDirection + 3) % 4;
                        break;
                    case ('+'):
                        // Apply directional change, then increment nextTurn
                        newDirection = (newDirection + turns[nextTurn]) % 4;
                        nextTurn = (nextTurn + 1) % 3;
                        break;
                }
                CurrentDirection = directionsL[newDirection];
                int[] vector = DirectionToVector(CurrentDirection);
                X += vector[0];
                Y += vector[1];
                return new int[] {X, Y};
            }

            int[] DirectionToVector(char c)
            {
                switch(c)
                {
                    case ('^'): return new int[] { 0, -1 };
                    case ('>'): return new int[] { 1, 0 };
                    case ('v'): return new int[] { 0, 1 };
                    case ('<'): return new int[] { -1, 0 };
                    default: return new int[0];
                }
            }

            public int CompareTo(object obj)
            {
                if (obj == null) return 1;

                Cart other = obj as Cart;

                return Y.CompareTo(other.Y) == 0 ? X.CompareTo(other.X) : Y.CompareTo(other.Y);
            }
        }
    }
}
