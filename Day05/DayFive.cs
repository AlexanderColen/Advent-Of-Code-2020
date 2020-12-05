using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day05
{
    public class DayFive : IDay
    {
        List<string> boardingPasses = new List<string>();

        public DayFive()
        {
            ReadInput();
            Puzzle1();
            Puzzle2();
        }

        public void Puzzle1()
        {
            var solution = 0;

            foreach (var pass in boardingPasses)
            {
                var row = FindSpot(string.Join("", pass.Take(7)), 128);
                var column = FindSpot(string.Join("", pass.Skip(7)), 8);
                var seatId = row * 8 + column;
                // Set highest.
                solution = seatId > solution ? seatId : solution;
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
            using StreamReader sr = new StreamReader(@"Day05/input.txt");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                boardingPasses.Add(line);
            }
        }

        private int FindSpot(string instructions, int arrayLength)
        {
            var range = Enumerable.Range(0, arrayLength).ToList();

            foreach (var c in instructions)
            {
                if (c == 'F' || c == 'L')
                {
                    range = range.Take(range.Count / 2).ToList();
                } else
                {
                    range = range.Skip(range.Count / 2).ToList();
                }
            }

            return range[0];
        }
    }
}