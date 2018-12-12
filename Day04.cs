using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day04
    {
        public int StrategyOne()
        {
            string[] lines = System.IO.File.ReadAllLines(@"input/Day04.txt");
            Array.Sort(lines);  // dates in ISO format, natively sorts how we want
            Dictionary<int, Guard> guards = AssembleGuards(lines);

            int maxID = 0;
            int[] maxReport = new int[2];
            foreach(int g in guards.Keys)
            {
                int[] report = guards[g].GuardReport();
                if (report[0] > maxReport[0])
                {
                    maxReport = report;
                    maxID = g;
                }
            }

            return maxID * maxReport[1];
        }

        public int StrategyTwo()
        {
            string[] lines = System.IO.File.ReadAllLines(@"input/Day04.txt");
            Array.Sort(lines);  // dates in ISO format, natively sorts how we want
            Dictionary<int, Guard> guards = AssembleGuards(lines);

            int maxID = 0;
            int[] maxFM = new int[2];
            foreach(int g in guards.Keys)
            {
                int[] fm = guards[g].FrequentMinute();
                if (fm[1] > maxFM[1])
                {
                    maxFM = fm;
                    maxID = g;
                }
            }

            return maxID * maxFM[0];
        }

        private Dictionary<int, Guard> AssembleGuards(string[] lines)
        {
            Dictionary<int, Guard> guards = new Dictionary<int, Guard>();

            for (int i = 0; i < lines.Length;)
            {
                int id = int.Parse(lines[i].Split(' ')[3].Substring(1));
                i++;
                int[] minutes = new int[60];
                while (i < lines.Length && !lines[i].Contains("begins"))
                {
                    int sleep = int.Parse(lines[i].Substring(15, 2));
                    int awake = int.Parse(lines[i + 1].Substring(15, 2));
                    for (; sleep < awake; sleep++)
                    {
                        minutes[sleep] += 1;
                    }
                    i += 2;
                }
                if (!guards.ContainsKey(id))
                {
                    guards.Add(id, new Guard(id));
                }
                guards[id].AddNight(minutes);
            }

            return guards;
        }
    }

    public class Guard
    {
        int id;
        int[] minutesAsleep; // each index counts occurrences guard was asleep that minute

        public Guard(int id) {
            this.id = id;
            minutesAsleep = new int[60];
        }

        public void AddNight(int[] night)
        {
            for (int i = 0; i < night.Length; i++)
            {
                minutesAsleep[i] += night[i];
            }
        }

        public int[] GuardReport()
        {
            int count = 0;
            int max = 0;
            for (int i = 0; i < minutesAsleep.Length; i++)
            {
                count += minutesAsleep[i];
                if (minutesAsleep[i] > minutesAsleep[max])
                {
                    max = i;
                }
            }
            return new int[]{ count, max };
        }

        public int[] FrequentMinute()
        {
            int max = 0;
            for (int i = 0; i < minutesAsleep.Length; i++)
            {
                if (minutesAsleep[i] > minutesAsleep[max])
                {
                    max = i;
                }
            }
            return new int[] { max, minutesAsleep[max] };
        }

        public override string ToString()
        {
            string s = id + "\t";
            foreach(int i in minutesAsleep)
            {
                s += i + ".";
            }
            return s;
        }
    }
}
