using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day09
    {
        public int HighScore()
        {
            string[] input = System.IO.File.ReadAllLines(@"input/Day09.txt")[0].Split(' ');
            int playerCount = int.Parse(input[0]);
            int targetScore = int.Parse(input[6]);

            int[] players = new int[playerCount];
            List<int> marbles = new List<int>(new int[1]);
            int currentMarble = 0;

            for (int i = 1; i < targetScore; i++)
            {
                if (i % 23 == 0)
                {
                    int sevenClockwise = (currentMarble + marbles.Count - 7) % marbles.Count;
                    int points = i + marbles[sevenClockwise];
                    players[i % playerCount] += points;
                    marbles.RemoveAt(sevenClockwise);
                    currentMarble = sevenClockwise;
                }
                else
                {
                    currentMarble = (currentMarble + 2) % marbles.Count;
                    marbles.Insert(currentMarble, i);
                }
            }

            Array.Sort(players, (int x, int y) => -(x.CompareTo(y)));
            return players[0];
        }

        public int HigherScore()
        {
            string[] input = System.IO.File.ReadAllLines(@"input/Day09.txt")[0].Split(' ');
            int playerCount = int.Parse(input[0]);
            int targetScore = int.Parse(input[6]) * 100;

            int[] players = new int[playerCount];
            LinkedList<int> marbles = new LinkedList<int>(new int[1]);

            for (int i = 1; i < targetScore; i++)
            {
                if (i % 23 == 0)
                {
                    for(int j = 0; j < 7; j++)
                    {
                        marbles.AddFirst(marbles.Last());
                        marbles.RemoveLast();
                    }
                    int points = i + marbles.Last();
                    players[i % playerCount] += points;
                    marbles.RemoveLast();
                    marbles.AddLast(marbles.First());
                    marbles.RemoveFirst();
                }
                else
                {
                    marbles.AddLast(marbles.First());
                    marbles.RemoveFirst();
                    marbles.AddLast(i);
                }
            }

            Array.Sort(players, (int x, int y) => -(x.CompareTo(y)));
            return players[0];
        }
    }
}
