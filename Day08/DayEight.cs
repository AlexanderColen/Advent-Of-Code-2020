using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020.Day08
{
    public class DayEight : IDay
    {
        private readonly List<string> instructions = new List<string>();

        public DayEight()
        {
            ReadInput();
            Puzzle1();
            Puzzle2();
        }

        public void Puzzle1()
        {
            var solution = 0;
            var visited = new List<int>();

            for (int i = 0; i < instructions.Count; i++)
            {
                if (visited.Contains(i))
                {
                    break;
                }

                // nop = skip to next
                if (instructions[i].StartsWith("nop"))
                {
                    visited.Add(i);
                    continue;
                }
                // acc = add to solution and skip to next
                else if (instructions[i].StartsWith("acc"))
                {
                    visited.Add(i);
                    solution += int.Parse(instructions[i][3..]);
                    continue;
                }
                // jmp = jump ahead/behind for next
                else if (instructions[i].StartsWith("jmp"))
                {
                    visited.Add(i);
                    i += int.Parse(instructions[i][3..]) - 1;
                    continue;
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
            using StreamReader sr = new StreamReader(@"Day08/input.txt");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                instructions.Add(line);
            }
        }
    }
}