using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day06
{
    public class DaySix : IDay
    {
        private readonly List<string> condensedGroupAnswers = new List<string>();
        private readonly List<List<string>> groupAnswers = new List<List<string>>();

        public DaySix()
        {
            ReadInput();
            Puzzle1();
            Puzzle2();
        }

        public void Puzzle1()
        {
            var solution = 0;

            foreach (var a in condensedGroupAnswers)
            {
                solution += new HashSet<char>(a).Count;
            }

            Console.WriteLine($"Puzzle 1 solution: {solution}");
        }

        public void Puzzle2()
        {
            var solution = 0;

            // Loop over every group.
            foreach (var g in groupAnswers)
            {
                IEnumerable<char> intersection = g[0];

                // Get the intersection of all lists.
                for (int i = 0; i < g.Count; i++)
                {
                    if (i + 1 <= g.Count - 1)
                    {
                        intersection = intersection.Intersect(g[i + 1]).ToList();
                    }
                }

                solution += intersection.Count();
            }

            Console.WriteLine($"Puzzle 2 solution: {solution}");
        }

        public void ReadInput()
        {
            using StreamReader sr = new StreamReader(@"Day06/input.txt");
            string line;
            var current = "";
            var currentGroup = new List<string>();
            while ((line = sr.ReadLine()) != null)
            {
                if (line != "")
                {
                    current += line;
                    currentGroup.Add(line);
                } else
                {
                    condensedGroupAnswers.Add(current);
                    current = "";
                    groupAnswers.Add(currentGroup);
                    currentGroup = new List<string>();
                }

                // Also add the final group.
                if (sr.Peek() == -1)
                {
                    condensedGroupAnswers.Add(current);
                    groupAnswers.Add(currentGroup);
                }
            }
        }
    }
}