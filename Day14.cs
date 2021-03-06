﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    class Day14
    {

        public string NextTenScores()
        {
            List<int> scores = new List<int>(new int[] { 3, 7 });
            int elf1 = 0;
            int elf2 = 1;
            int input = 765071;

            while (scores.Count < input + 10)
            {
                // add current scores together
                int newScore = scores[elf1] + scores[elf2];
                // split total into digits
                // add each digit to list
                foreach (char c in newScore + "") scores.Add(int.Parse(c + ""));
                // elf 1 moves (current score + 1) forwards
                elf1 = (elf1 + scores[elf1] + 1) % scores.Count;
                // elf 2 moves (current score + 1) forwards
                elf2 = (elf2 + scores[elf2] + 1) % scores.Count;
            }

            string s = "";
            for (int i = 0; i < 10; i++)
            {
                s += scores[input + i];
            }
            return s;
        }

        public int TimeUntilScore()
        {
            List<int> scores = new List<int>(new int[] { 3, 7 });
            int elf1 = 0;
            int elf2 = 1;
            int input = 765071;

            while (true)
            {
                int newScore = scores[elf1] + scores[elf2];
                foreach (char c in newScore + ""){
                    scores.Add(int.Parse(c + ""));
                    if (CheckSuccess(scores, input)) return scores.Count - (input + "").Length;
                } 
                elf1 = (elf1 + scores[elf1] + 1) % scores.Count;
                elf2 = (elf2 + scores[elf2] + 1) % scores.Count;
            }
        }

        bool CheckSuccess(List<int> scores, int input)
        {
            int inputLength = (input + "").Length;
            if (scores.Count < inputLength) return false;
            for (int i = 0; i < inputLength; i++)
            {
                if (scores[scores.Count - (i+1)] != input % 10) return false;
                input = input / 10;
            }
            return true;
        }
    }
}
