using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day10
{
    public class DayTen : IDay
    {
        private readonly List<long> joltages = new List<long>();

        public DayTen()
        {
            ReadInput();
            Puzzle1();
            Puzzle2();
        }

        public void Puzzle1()
        {
            long current = 0;
            var joltDifferences = new List<long>();
            joltages.Sort();
            joltages.Add(joltages.Max() + 3);

            foreach (var j in joltages)
            {
                // Max difference allowed is 3.
                if (j - current <= 3)
                {
                    joltDifferences.Add(j - current);
                    current = j;
                }
            }

            Console.WriteLine($"Puzzle 1 solution: {joltDifferences.Count(x => x == 1) * joltDifferences.Count(x => x == 3)}");
        }

        public void Puzzle2()
        {
            var solution = 0;

            Console.WriteLine($"Puzzle 2 solution: {solution}");
        }

        public void ReadInput()
        {
            using StreamReader sr = new StreamReader(@"Day10/input.txt");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                joltages.Add(long.Parse(line));
            }
        }
    }
}