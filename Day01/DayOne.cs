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
            List<int> checks = new List<int>();
            while (solution == 0) {
                foreach (var i in input)
                {
                    if (i > 2020)
                    {
                        continue;
                    }
                    else
                    {
                        foreach (var j in checks)
                        {
                            if (i + j == 2020)
                            {
                                solution = i * j;
                            }
                        }
                        checks.Add(i);
                    }
                }
            }
            Console.WriteLine($"Puzzle 1 solution: {solution}");
        }

        public void Puzzle2()
        {
            int solution = 0;
            List<int> checksI = new List<int>();
            List<int> checksJ = new List<int>();
            while (solution == 0) {
                foreach (var i in input)
                {
                    if (i > 2020)
                    {
                        continue;
                    }
                    else
                    {
                        foreach (var j in checksI)
                        {
                            foreach (var k in checksJ)
                            {
                                if (i + j + k == 2020)
                                {
                                    solution = i * j * k;
                                }
                            }
                            checksJ.Add(j);
                        }
                        checksI.Add(i);
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