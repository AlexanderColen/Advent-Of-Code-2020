using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day07
{
    public class DaySeven : IDay
    {
        private readonly List<string> rules = new List<string>();
        private readonly List<Bag> bags = new List<Bag>();

        public DaySeven()
        {
            ReadInput();
            Puzzle1();
            Puzzle2();
        }

        public void Puzzle1()
        {
            var solution = 0;

            foreach (var rule in rules)
            {
                // Skip useless rules.
                if (rule.Contains("no other bags") || rule.StartsWith("shiny gold")) {
                    continue;
                }

                bags.Add(CreateBag(rule));
            }

            foreach (var bag in bags)
            {
                solution += CountBagContents(bag);
            }

            Console.WriteLine($"Puzzle 1 solution: {solution}");
        }

        public void Puzzle2()
        {
            var solution = 0;

            foreach (var rule in rules)
            {
                // Skip useless rules.
                if (!rule.StartsWith("shiny gold")) {
                    continue;
                }

                var shinyGoldBag = CreateBag(rule);
                solution = CountTotalBags(shinyGoldBag);
            }

            Console.WriteLine($"Puzzle 2 solution: {solution}");
        }

        public void ReadInput()
        {
            using StreamReader sr = new StreamReader(@"Day07/input.txt");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                rules.Add(line);
            }
        }

        private Bag CreateBag(string rule)
        {
            Bag b = new Bag()
            {
                Colour = rule.Split(" contain")[0].Replace(" bags", ""),
                HasDirectShiny = rule.Split(" contain")[1].Contains("shiny gold")
            };

            if (!b.HasDirectShiny)
            {
                Regex re = new Regex(@"\d ([\w\s]+)(?= bag)");
                foreach (var m in re.Matches(rule))
                {
                    var a = int.Parse(m.ToString().Substring(0, 1));
                    var c = m.ToString()[2..];

                    foreach (var r in rules)
                    {
                        if (r.Split(" contain")[0].Replace(" bags", "") == c)
                        {
                            b.Contents.Add(new Tuple<Bag, int>(CreateBag(r), a));
                        }
                    }
                }
            }

            return b;
        }

        private int CountBagContents(Bag bag)
        {
            var count = 0;

            if (bag.HasDirectShiny)
            {
                count++;
            } else
            {
                foreach (var b in bag.Contents)
                {
                    if (b.Item1.HasDirectShiny)
                    {
                        count++;
                        break;
                    }
                    else if (CountBagContents(b.Item1) > 0)
                    {
                        count++;
                        break;
                    }
                }
            }

            return count;
        }

        private int CountTotalBags(Bag bag)
        {
            var count = 0;

            foreach (var b in bag.Contents)
            {
                count += b.Item2 + b.Item2 * CountTotalBags(b.Item1);
            }

            return count;
        }
    }
}