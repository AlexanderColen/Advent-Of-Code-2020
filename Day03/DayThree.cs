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
            var solution = CheckSlopes(3, 1);

            Console.WriteLine($"Puzzle 1 solution: {solution}");
        }

        public void Puzzle2()
        {
            long solution = CheckSlopes(1, 1) * CheckSlopes(3, 1) * CheckSlopes(5, 1) * CheckSlopes(7, 1) * CheckSlopes(1, 2);

            Console.WriteLine($"Puzzle 2 solution: {solution}");
        }

        private long CheckSlopes(int step_x, int step_y)
        {
            var solution = 0;
            var last_x = 0;
            var last_y = 0;
            int step;
            for (var y = 0; y < grid.Count; y++)
            {
                if (y < last_y + step_y)
                {
                    continue;
                } else
                {
                    last_y = y;
                }

                // Calculate next x position to check.
                step = last_x + step_x;
                if (step >= grid[y].Length)
                {
                    step -= grid[y].Length;
                }
                for (int x = 0; x < grid[y].Length; x++)
                {
                    if (x == step)
                    {
                        if (grid[y][x] == '#')
                        {
                            solution++;
                        }
                        last_x = x;
                        break;
                    }
                }
            }

            return solution;
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