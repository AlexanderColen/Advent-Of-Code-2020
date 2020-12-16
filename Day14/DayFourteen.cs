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
        private readonly Dictionary<int, string> memory = new Dictionary<int, string>();
        private readonly Dictionary<string, ulong> memory2 = new Dictionary<string, ulong>();

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
            var mask = "0";

            foreach (var instruction in instructions)
            {
                // Change mask.
                if (instruction.StartsWith("mask"))
                {
                    mask = instruction[7..].PadLeft(64, '0');
                }
                // Write value to memory.
                else if (instruction.StartsWith("mem"))
                {
                    var matches = new Regex(@"\d+").Matches(instruction);
                    var originalAddress = Convert.ToString(int.Parse(matches[0].Value), 2).PadLeft(64, '0');
                    var value = ulong.Parse(matches[1].Value);

                    // Write for every address.
                    foreach (var a in ChangeBits(mask, originalAddress.ToCharArray(), 0)) {
                        memory2[a] = value;
                    }
                }
            }
            
            ulong solution = memory2.Select(m => m.Value).Aggregate((a, b) => a + b);

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

        private List<string> ChangeBits(string mask, char[] input, int currentIndex)
        {
            var addresses = new List<string>();

            for (var i = currentIndex; i < mask.Length; i++)
            {
                // X bit (floating).
                if (mask[i] == 'X')
                {
                    // Treat as a 1.
                    var input1 = input;
                    input1[i] = '1';
                    addresses.AddRange(ChangeBits(mask, input1, i + 1));
                    // Treat as a 0.
                    var input0 = input;
                    input0[i] = '0';
                    addresses.AddRange(ChangeBits(mask, input0, i + 1));

                }
                // 1 bit (override)
                else if (mask[i] == '1')
                {
                    input[i] = '1';
                }
            }

            addresses.Add(string.Join("", input));

            return addresses;
        }
    }
}