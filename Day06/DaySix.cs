using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020.Day06
{
    public class DaySix : IDay
    {
        private readonly List<HashSet<char>> groupAnswers = new List<HashSet<char>>();

        public DaySix()
        {
            ReadInput();
            Puzzle1();
            Puzzle2();
        }

        public void Puzzle1()
        {
            var solution = 0;

            foreach (var a in groupAnswers)
            {
                solution += a.Count;
            }

            Console.WriteLine($"Puzzle 1 solution: {solution}");
        }

        public void Puzzle2()
        {
            var solution = 0;

            Console.WriteLine($"Puzzle 2 solution: {solution}");
        }

        public void ReadInput()
        {
            using StreamReader sr = new StreamReader(@"Day06/input.txt");
            string line;
            var current = "";
            while ((line = sr.ReadLine()) != null)
            {
                if (line != "")
                {
                    current += line;
                } else
                {
                    groupAnswers.Add(new HashSet<char>(current));
                    current = "";
                }

                // Also add the final group.
                if (sr.Peek() == -1)
                {
                    groupAnswers.Add(new HashSet<char>(current));
                }
            }
        }
    }
}