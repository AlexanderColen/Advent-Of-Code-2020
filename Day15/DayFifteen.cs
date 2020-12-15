using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day15
{
    public class DayFifteen : IDay
    {
        private List<int> spokenNumbers = new List<int>();

        public DayFifteen()
        {
            ReadInput();
            Puzzle1();
            Puzzle2();
        }

        public void Puzzle1()
        {
            while (spokenNumbers.Count < 2020)
            {
                if (spokenNumbers.Count(x => x == spokenNumbers[^1]) == 1)
                {
                    spokenNumbers.Add(0);
                } else
                {
                    spokenNumbers.Add(spokenNumbers.Count - spokenNumbers.SkipLast(1).ToList().LastIndexOf(spokenNumbers[^1]) - 1);
                }
            }

            Console.WriteLine($"Puzzle 1 solution: {spokenNumbers[^1]}");
        }

        public void Puzzle2()
        {
            var solution = 0;

            Console.WriteLine($"Puzzle 2 solution: {solution}");
        }

        public void ReadInput()
        {
            var input = "14,1,17,0,3,20";
            spokenNumbers.AddRange(input.Split(',').Select(n => int.Parse(n)));
        }
    }
}