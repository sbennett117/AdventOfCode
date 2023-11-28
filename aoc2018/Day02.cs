using System;
using System.Collections.Generic;
namespace AdventOfCode
{
    public class Day02
    {
        public int Checksum()
        {
            int threeOfAKind = 0;
            int twoOfAKind = 0;

            string[] lines = System.IO.File.ReadAllLines(@"input/Day02.txt");
            foreach(string line in lines)
            {
                ISet<char> uniqueChars = new HashSet<char>(line.ToCharArray());
                bool threeFound = false;
                bool twoFound = false;
                foreach(char c in uniqueChars)
                {
                    int index = line.IndexOf(c);
                    if (line.IndexOf(c, index+1) != -1)
                    {
                        index = line.IndexOf(c, index + 1);
                        if (line.IndexOf(c, index + 1) != -1)
                        {
                            index = line.IndexOf(c, index + 1);
                            if (!threeFound && line.IndexOf(c, index + 1) == -1)
                            {
                                threeFound = true;
                                threeOfAKind++;
                            }
                        } else if (!twoFound) {
                            twoFound = true;
                            twoOfAKind++;
                        }
                    }
                }
            }
            Console.WriteLine(twoOfAKind + " " + threeOfAKind);

            return threeOfAKind * twoOfAKind;
        }

        public string GetIDs()
        {
            string[] lines = System.IO.File.ReadAllLines(@"input/Day02.txt");
            foreach(string a in lines)
            {
                foreach(string b in lines)
                {
                    int parity = 0;
                    for (int i = 0; i < a.Length; i++)
                    {
                        if (a[i] != b[i])
                        {
                            parity++;
                        }
                    }
                    if (parity == 1)
                    {
                        return a + "\n" + b;
                    }
                }
            }
            return "";
        }
    }
}
