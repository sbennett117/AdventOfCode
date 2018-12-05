using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day05
    {
        public int ReducePolymer()
        {
            string units = System.IO.File.ReadAllLines(@"Day05.txt")[0];
            List<char> polymer = new List<char>(units);
            int recordedLength = polymer.Count + 1; // rL >= Length

            while (recordedLength > polymer.Count)
            {
                recordedLength = polymer.Count;
                for (int i = 0; i < polymer.Count - 1;)
                {
                    if (char.ToLower(polymer[i]) == char.ToLower(polymer[i + 1]) && polymer[i] != polymer[i + 1])
                    {
                        polymer.RemoveAt(i);
                        polymer.RemoveAt(i);
                    }
                    else
                    {
                        i++;
                    }
                }
            }

            return recordedLength;
        }

        public int SplicePolymer()
        {
            string units = System.IO.File.ReadAllLines(@"Day05.txt")[0];
            ISet<char> uniqueUnits = new HashSet<char>();
            foreach (char c in units.ToLower())
            {
                uniqueUnits.Add(c);
            }

            int shortestLength = units.Length;
            foreach (char c in uniqueUnits)
            {
                List<char> polymer = new List<char>(units);
                polymer.RemoveAll((char obj) => obj == c || obj == char.ToUpper(c));
                int recordedLength = polymer.Count + 1; // rL >= Length

                while (recordedLength > polymer.Count)
                {
                    recordedLength = polymer.Count;
                    for (int i = 0; i < polymer.Count - 1;)
                    {
                        if (char.ToLower(polymer[i]) == char.ToLower(polymer[i + 1]) && polymer[i] != polymer[i + 1])
                        {
                            polymer.RemoveAt(i);
                            polymer.RemoveAt(i);
                        }
                        else
                        {
                            i++;
                        }
                    }
                }

                shortestLength = Math.Min(shortestLength, recordedLength);
            }

            return shortestLength;
        }
    }
}
