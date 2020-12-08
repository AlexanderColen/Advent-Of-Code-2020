using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020.Day08
{
    public class DayEight : IDay
    {
        private readonly List<string> defaultInstructions = new List<string>();
        private readonly string ACC = "acc";
        private readonly string NOP = "nop";
        private readonly string JMP = "jmp";

        public DayEight()
        {
            ReadInput();
            Puzzle1();
            Puzzle2();
        }

        public void Puzzle1()
        {
            var solution = FollowInstructions(defaultInstructions);

            Console.WriteLine($"Puzzle 1 solution: {solution.Item1}");
        }

        public void Puzzle2()
        {
            var solution = 0;

            for (var i = 0; i < defaultInstructions.Count; i++)
            {
                // Change a nop to jmp
                if (defaultInstructions[i].StartsWith(NOP))
                {
                    defaultInstructions[i] = defaultInstructions[i].Replace(NOP, JMP);
                    var tuple = FollowInstructions(defaultInstructions);
                    // Break out if it was not an infinite loop.
                    if (!tuple.Item2)
                    {
                        solution = tuple.Item1;
                        break;
                    }
                    // Reset to original instruction.
                    defaultInstructions[i] = defaultInstructions[i].Replace(JMP, NOP);
                }
                // Change a jmp to nop
                else
                {
                    defaultInstructions[i] = defaultInstructions[i].Replace(JMP, NOP);
                    var tuple = FollowInstructions(defaultInstructions);
                    // Break out if it was not an infinite loop.
                    if (!tuple.Item2)
                    {
                        solution = tuple.Item1;
                        break;
                    }
                    // Reset to original instruction.
                    defaultInstructions[i] = defaultInstructions[i].Replace(NOP, JMP);
                }
            }

            Console.WriteLine($"Puzzle 2 solution: {solution}");
        }

        public void ReadInput()
        {
            using StreamReader sr = new StreamReader(@"Day08/input.txt");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                defaultInstructions.Add(line);
            }
        }

        private Tuple<int, bool> FollowInstructions(List<string> instructions)
        {
            var visited = new List<int>();
            var outcome = 0;
            var infiniteLoop = false;

            for (int i = 0; i < instructions.Count; i++)
            {
                if (visited.Contains(i))
                {
                    infiniteLoop = true;
                    break;
                }

                // nop = skip to next
                if (instructions[i].StartsWith(NOP))
                {
                    visited.Add(i);
                    continue;
                }
                // acc = add to solution and skip to next
                else if (instructions[i].StartsWith(ACC))
                {
                    visited.Add(i);
                    outcome += int.Parse(instructions[i][3..]);
                    continue;
                }
                // jmp = jump ahead/behind for next
                else if (instructions[i].StartsWith(JMP))
                {
                    visited.Add(i);
                    i += int.Parse(instructions[i][3..]) - 1;
                    continue;
                }
            }

            return new Tuple<int, bool>(outcome, infiniteLoop);
        }
    }
}