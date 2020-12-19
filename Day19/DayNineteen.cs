using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day19
{
    public class DayNineteen : IDay
    {
        private readonly Dictionary<int, string> rules = new Dictionary<int, string>();
        private readonly List<string> messages = new List<string>();

        public DayNineteen()
        {
            ReadInput();
            Puzzle1();
            Puzzle2();
        }

        public void Puzzle1()
        {
            var solution = 0;

            var zero = rules[0];
            var re = new Regex(@"\d+");
            var outcome = SubstituteRules(zero, re).Replace(" ", "");

            var re2 = new Regex($"^({outcome})$");
            foreach (var message in messages)
            {
                solution += re2.IsMatch(message) ? 1 : 0;
            }

            Console.WriteLine($"Puzzle 1 solution: {solution}");
        }

        public void Puzzle2()
        {
            var solution = 0;

            // Edit rule 8 into 8: 42 | 42 8 and rule 11 into 11: 42 31 | 42 11 31
            // 42 | 42 8 (replacing 8 with this is just a string of 42s)
            rules[8] = " 42 +";
            // 42 31 | 42 11 31 (Potentially infinite repeats of 42 followed by 42 x times and 31 x times followed by 31.)
            rules[11] = " 42 31 ";
            // 5 repeats is the magic number for my input.
            var repeats = 5;
            for (var i = 1; i < repeats; i++)
            {
                rules[11] = $"{rules[11]}|{string.Concat(Enumerable.Repeat(" 42 ", i))}{string.Concat(Enumerable.Repeat(" 31 ", i))}";
            }

            var zero = rules[0];
            var re = new Regex(@"\d+");
            var outcome = SubstituteRules(zero, re).Replace(" ", "");

            var re2 = new Regex($"^{outcome}$");
            foreach (var message in messages)
            {
                solution += re2.IsMatch(message) ? 1 : 0;
            }

            Console.WriteLine($"Puzzle 2 solution: {solution}");
        }

        public void ReadInput()
        {
            using StreamReader sr = new StreamReader(@"Day19/input.txt");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if (line == "")
                {
                    continue;
                }

                if (line.StartsWith('a') || line.StartsWith('b'))
                {
                    messages.Add(line);
                } else
                {
                    var parts = line.Split(':');
                    rules.Add(int.Parse(parts[0]), parts[1].Replace("\"", "") + ' ');
                }
            }
        }

        private string SubstituteRules(string rule, Regex regex)
        {
            foreach (var match in regex.Matches(rule)) {
                if (!rule.Contains(match.ToString()))
                {
                    continue;
                }
                var num = int.Parse(match.ToString());
                var outcome = SubstituteRules(rules[num], regex);
                if (outcome.Length != 3)
                {
                    outcome = $"({outcome})";
                }
                rule = rule.Replace($" {num} ", $" {outcome} ");
            }

            return rule;
        }
    }
}