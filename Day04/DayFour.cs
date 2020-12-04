using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day04
{
    public class DayFour : IDay
    {
        List<Dictionary<string, string>> passports;
        private readonly List<string> validEcl = new List<string> {
            "amb",
            "blu",
            "brn",
            "gry",
            "grn",
            "hzl",
            "oth"
        };

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

            foreach (var p in passports)
            {
                // Don't bother with passports that were not valid in puzzle 1.
                if (p.Keys.Count == 8 || (p.Keys.Count == 7 && !p.Keys.Contains("cid")))
                {
                    var validFields = 0;
                    try
                    {
                        foreach (var kv in p)
                        {
                            // Skip cid since it is optional.
                            if (kv.Key == "cid")
                            {
                                continue;
                            }
                            // Birth year: between 1920 and 2002 inclusive.
                            else if (kv.Key == "byr" && int.Parse(kv.Value) >= 1920 && int.Parse(kv.Value) <= 2002)
                            {
                                validFields++;
                            }
                            // Issue year: between 2010 and 2020 inclusive.
                            else if (kv.Key == "iyr" && int.Parse(kv.Value) >= 2010 && int.Parse(kv.Value) <= 2020)
                            {
                                validFields++;
                            }
                            // Expiration year: between 2020 and 2030 inclusive.
                            else if (kv.Key == "eyr" && int.Parse(kv.Value) >= 2020 && int.Parse(kv.Value) <= 2030)
                            {
                                validFields++;
                            }
                            // Height: between 150 and 193 if followed by 'cm' or between 59 and 76 followed by 'in' inclusive.
                            else if (kv.Key == "hgt")
                            {
                                if (kv.Value.EndsWith("cm") && int.Parse(kv.Value.Replace("cm", "")) >= 150 && int.Parse(kv.Value.Replace("cm", "")) <= 193) {
                                    validFields++;
                                }
                                else if (kv.Value.EndsWith("in") && int.Parse(kv.Value.Replace("in", "")) >= 59 && int.Parse(kv.Value.Replace("in", "")) <= 76) {
                                    validFields++;
                                }
                            }
                            // Hair colour: hex colour.
                            else if (kv.Key == "hcl" && kv.Value.StartsWith('#') && int.Parse(kv.Value.Replace("#", ""), System.Globalization.NumberStyles.HexNumber) != -1)
                            {
                                validFields++;
                            }
                            // Eye colour: colour in validEcl.
                            else if (kv.Key == "ecl" && validEcl.Contains(kv.Value))
                            {
                                validFields++;
                            }
                            // Passport ID: nine digit number with leading zeroes.
                            else if (kv.Key == "pid" && kv.Value.Length == 9 && int.Parse(kv.Value) != 0)
                            {
                                validFields++;
                            }
                        }

                        if (validFields == 7)
                        {
                            solution++;
                        }
                    } catch (FormatException)
                    {
                        // Invalid pid or eyr, so passport is invalid.
                        continue;
                    }
                }
            }

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