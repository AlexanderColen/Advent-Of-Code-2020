using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day14
{
    public class DayFourteen : IDay
    {
        private readonly List<string> instructions = new List<string>();
        private Dictionary<int, string> memory = new Dictionary<int, string>();

        public DayFourteen()
        {
            ReadInput();
            Puzzle1();
            Puzzle2();
        }

        public void Puzzle1()
        {
            var mask = "0";

            foreach (var instruction in instructions)
            {
                // Change mask.
                if (instruction.StartsWith("mask"))
                {
                    mask = instruction[7..].PadLeft(64, 'X');
                }
                // Write value to memory.
                else if (instruction.StartsWith("mem"))
                {
                    var matches = new Regex(@"\d+").Matches(instruction);
                    var address = int.Parse(matches[0].Value);
                    var value = int.Parse(matches[1].Value);

                    // Set binary value.
                    memory[address] = Convert.ToString(value, 2).PadLeft(64, '0');
                    // Override with mask.
                    var newValue = memory[address].ToCharArray();
                    for (var i = mask.Length - 1; i > -1; i--)
                    {
                        if (mask[i] != 'X')
                        {
                            newValue[i] = mask[i];
                        }
                    }
                    memory[address] = string.Join("", newValue);
                }
            }

            var solution = memory.Select(m => Convert.ToUInt64(m.Value, 2)).Aggregate((a, b) => a + b);

            Console.WriteLine($"Puzzle 1 solution: {solution}");
        }

        public void Puzzle2()
        {
            var solution = 0;

            Console.WriteLine($"Puzzle 2 solution: {solution}");
        }

        public void ReadInput()
        {
            using StreamReader sr = new StreamReader(@"Day14/input.txt");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                instructions.Add(line);
            }
        }
    }
}