using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day12
    {
        public int SumOfPots(long noOfGens)
        {
            string[] result = SimulateGenerations(noOfGens).Split(' ');
            int count = 0;
            int firstIndex = int.Parse(result[0]);
            string pots = result[1];

            for (int i = 0; i < pots.Length; i++) if (pots[i] == '#') count += i + firstIndex;

            return count;
        }

        public string SimulateGenerations(long noOfGens)
        {
            string[] input = System.IO.File.ReadAllLines(@"input/Day12.txt");
            string pots = input[0].Split(' ')[2];
            int firstIndex = 0;
            Dictionary<string, char> rules = new Dictionary<string, char>();
            for (int i = 2; i < input.Length; i++)
            {
                string[] splitLine = input[i].Split(" => ");
                rules.Add(splitLine[0], splitLine[1][0]);
            }

            for (int i = 0; i < noOfGens; i++)
            {
                while (!pots.StartsWith("....", StringComparison.CurrentCulture))
                {
                    pots = "." + pots;
                    firstIndex--;
                }
                while (!pots.EndsWith("....", StringComparison.CurrentCulture)) pots = pots + ".";

                char[] potArray = pots.ToCharArray();
                for (int p = 0; p < pots.Length - 4; p++)
                {
                    potArray[p + 2] = rules.GetValueOrDefault(pots.Substring(p, 5), '.');
                }

                pots = new string(potArray);
            }

            return firstIndex + " " + pots;
        }
    }
}
