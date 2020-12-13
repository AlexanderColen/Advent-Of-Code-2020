using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day13
{
    public class DayThirteen : IDay
    {
        private long earliest;
        private List<string> busses = new List<string>();

        public DayThirteen()
        {
            ReadInput();
            Puzzle1();
            Puzzle2();
        }

        public void Puzzle1()
        {
            var solution = int.MaxValue;
            var busId = 0;

            foreach (var bus in busses.Where(b => b != "x"))
            {
                var next = Math.Ceiling(earliest / double.Parse(bus)) * int.Parse(bus);
                if (next - earliest < solution)
                {
                    solution = (int)(next - earliest);
                    busId = int.Parse(bus);
                }
            }

            Console.WriteLine($"Puzzle 1 solution: {solution * busId}");
        }

        public void Puzzle2()
        {
            var solution = 0;

            Console.WriteLine($"Puzzle 2 solution: {solution}");
        }

        public void ReadInput()
        {
            using StreamReader sr = new StreamReader(@"Day13/input.txt");
            earliest = long.Parse(sr.ReadLine());
            busses = sr.ReadLine().Split(',').ToList();
        }
    }
}