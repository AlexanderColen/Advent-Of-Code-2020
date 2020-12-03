using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day03
{
    public class DayThree : IDay
    {
        public List<char[]> grid;

        public DayThree()
        {
            ReadInput();
            Puzzle1();
            Puzzle2();
        }

        public void Puzzle1()
        {
            var solution = 0;
            var last = 0;
            int step;
            // Remove first row to skip.
            grid.Remove(grid[0]);
            foreach (var y in grid)
            {
                step = last + 3;
                if (step >= y.Length)
                {
                    step -= y.Length;
                }
                for (int x = 0; x < y.Length; x++)
                {
                    if (x == step)
                    {
                        if (y[x] == '#')
                        {
                            solution++;
                        }
                        last = x;
                        break;
                    }
                }
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
            using StreamReader sr = new StreamReader(@"Day03/input.txt");
            grid = new List<char[]>();
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                grid.Add(line.ToCharArray());
            }
        }
    }
}