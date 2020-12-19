using System;
using System.Collections.Generic;
using System.IO;
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