using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020.Day01
{
    public class DayOne : IDay
    {
        private List<int> input;

        public DayOne()
        {
            ReadInput();
            Puzzle1();
            Puzzle2();
        }

        public void Puzzle1()
        {
            int solution = 0;
            input.Sort();
            while (solution == 0) {
                foreach (var i in input)
                {
                    foreach (var j in input)
                    {
                        if (i + j == 2020)
                        {
                            solution = i * j;
                        }
                    }
                }
            }
            Console.WriteLine($"Puzzle 1 solution: {solution}");
        }

        public void Puzzle2()
        {
            int solution = 0;
            input.Sort();
            while (solution == 0) {
                foreach (var i in input)
                {
                    foreach (var j in input)
                    {
                        foreach (var k in input)
                        {
                            if (i + j + k == 2020)
                            {
                                solution = i * j * k;
                            }
                        }
                    }
                }
            }
            Console.WriteLine($"Puzzle 2 solution: {solution}");
        }

        public void ReadInput()
        {
            using StreamReader sr = new StreamReader(@"Day01/input.txt");
            input = new List<int>();
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                input.Add(int.Parse(line));
            }
        }
    }
}