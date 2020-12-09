using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day09
{
    public class DayNine : IDay
    {
        private readonly List<long> xmas = new List<long>();

        public DayNine()
        {
            ReadInput();
            Puzzle1();
            Puzzle2();
        }

        public void Puzzle1()
        {
            var solution = CheckPreamble(25);

            Console.WriteLine($"Puzzle 1 solution: {solution}");
        }

        public void Puzzle2()
        {
            var solution = 0;

            Console.WriteLine($"Puzzle 2 solution: {solution}");
        }

        public void ReadInput()
        {
            using StreamReader sr = new StreamReader(@"Day09/input.txt");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                xmas.Add(long.Parse(line));
            }
        }

        private long CheckPreamble(int preambleSize)
        {
            var checkIndex = 0;
            long found = 0;

            while (found == 0) {
                var sumTo = xmas[checkIndex + preambleSize];
                var foundValid = false;
                for (int i = checkIndex; i < checkIndex + preambleSize - 1; i++)
                {
                    for (int j = i + 1; j < checkIndex + preambleSize; j++)
                    {
                        if (!foundValid) {
                            foundValid = xmas[i] + xmas[j] == sumTo;
                            if (xmas[i] + xmas[j] == sumTo)
                            {
                                break;
                            }
                        }
                    }
                    
                }
                
                if (!foundValid)
                {
                    found = sumTo;
                    break;
                }

                checkIndex++;
            }

            return found;
        }
    }
}