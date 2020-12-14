using System;
using System.Collections.Generic;
using System.IO;
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
            ulong solution = 0;
            var defaultMask = "".PadLeft(64, '0').ToCharArray();

            foreach (var instruction in instructions)
            {
                // Overwrite bits in mask.
                if (instruction.StartsWith("mask"))
                {
                    var bits = instruction[7..].PadLeft(64, '0');
                    // Overwrite default mask.
                    for (var i = 0; i < bits.Length; i++)
                    {
                        if (bits[i] != 'X')
                        {
                            defaultMask[i] = bits[i];
                        }
                    }

                    var newMemory = new Dictionary<int, string>();
                    // Overwrite in address.
                    foreach (var address in memory.Keys)
                    {
                        var mask = memory[address].ToCharArray();
                        for (var i = 0; i < bits.Length; i++)
                        {
                            if (bits[i] != 'X')
                            {
                                mask[i] = bits[i];
                            }
                        }
                        newMemory.Add(address, string.Join("", mask));
                    }
                    memory = newMemory;
                }
                // Write value to memory.
                else if (instruction.StartsWith("mem"))
                {
                    var matches = new Regex(@"\d+").Matches(instruction);
                    var address = int.Parse(matches[0].Value);
                    var value = matches[1].Value;
                    // Add default mask to address if it's no initialized yet.
                    if (!memory.ContainsKey(address))
                    {
                        memory.Add(address, string.Join("", defaultMask));
                    }

                    memory[address] = Convert.ToString(Convert.ToInt64(memory[address], 2) + long.Parse(value), 2).PadLeft(64, '0');
                }
            }

            // Calculate total.
            foreach (var address in memory.Keys)
            {
                Console.WriteLine($"Address: {address} - Raw: {memory[address]} - Value: {Convert.ToUInt64(memory[address], 2)}");
                solution += Convert.ToUInt64(memory[address], 2);
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
            using StreamReader sr = new StreamReader(@"Day14/input.txt");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                instructions.Add(line);
            }
        }
    }
}