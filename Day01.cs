using System;
using System.Collections.Generic;
namespace AdventOfCode
{
    public class Day01
    {
        public int Calibrate()
        {
            int result = 0;
            string[] lines = System.IO.File.ReadAllLines(@"Day01.txt");
            foreach(string s in lines)
            {
                result += int.Parse(s);
            }
            return result;
        }

        public int Duplicate()
        {
            int frequency = 0;
            ISet<int> uniqueFreqs = new HashSet<int>();
            string[] lines = System.IO.File.ReadAllLines(@"Day01.txt");
            int index = 0;

            while (uniqueFreqs.Add(frequency))
            {
                frequency += int.Parse(lines[index]);
                index = (index + 1) % lines.Length;
            }

            return frequency;
        }
    }
}
