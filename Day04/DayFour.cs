using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day04
{
    public class DayFour : IDay
    {
        private readonly List<string> requiredFields = new List<string> {
            "byr",
            "cid",
            "ecl",
            "eyr",
            "hcl",
            "hgt",
            "iyr",
            "pid",
        };
        List<Dictionary<string, string>> passports;

        public DayFour()
        {
            ReadInput();
            Puzzle1();
            Puzzle2();
        }

        public void Puzzle1()
        {
            var solution = 0;

            foreach (var p in passports)
            {
                if (p.Keys.Count == 8 || (p.Keys.Count == 7 && !p.Keys.Contains("cid")))
                {
                    solution++;
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
            using StreamReader sr = new StreamReader(@"Day04/input.txt");
            string line;
            passports = new List<Dictionary<string, string>>();
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            while ((line = sr.ReadLine()) != null)
            {
                // Make sure to handle newlines.
                if (line != "")
                {
                    foreach (var kvPair in line.Split())
                    {
                        keyValuePairs.Add(kvPair.Split(':')[0], kvPair.Split(':')[1]);
                    }
                } else
                {
                    passports.Add(keyValuePairs);
                    keyValuePairs = new Dictionary<string, string>();
                }

                // Also add the final entry.
                if (sr.Peek() == -1)
                {
                    passports.Add(keyValuePairs);
                }
            }
        }
    }
}