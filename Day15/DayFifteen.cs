using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day15
{
    public class DayFifteen : IDay
    {
        private readonly List<int> spokenNumbers = new List<int>();
        private readonly Dictionary<int, Tuple<int, int>> numbers = new Dictionary<int, Tuple<int, int>>();

        public DayFifteen()
        {
            ReadInput();
            Puzzle1();
            Puzzle2();
        }

        public void Puzzle1()
        {
            while (spokenNumbers.Count < 2020)
            {
                // Last number was the first time it appeared.
                if (spokenNumbers.Count(x => x == spokenNumbers[^1]) == 1)
                {
                    spokenNumbers.Add(0);
                }
                // How many turns ago did the last number appear before that.
                else
                {
                    spokenNumbers.Add(spokenNumbers.Count - spokenNumbers.SkipLast(1).ToList().LastIndexOf(spokenNumbers[^1]) - 1);
                }
            }

            Console.WriteLine($"Puzzle 1 solution: {spokenNumbers[^1]}");
        }

        public void Puzzle2()
        {
            var last = 20;
            var turn = numbers.Count;
            while (turn < 30000000)
            {
                turn++;
                // Last number was the first time it appeared.
                if (numbers[last].Item1 == -1)
                {
                    if (numbers.ContainsKey(0))
                    {
                        numbers[0] = new Tuple<int, int>(numbers[0].Item2, turn);
                    } else
                    {
                        numbers.Add(0, new Tuple<int, int>(-1, turn));
                    }
                    
                    last = 0;
                }
                // How many turns ago did the last number appear before that.
                else
                {
                    var age = numbers[last].Item2 - numbers[last].Item1;
                    if (numbers.ContainsKey(age))
                    {
                        numbers[age] = new Tuple<int, int>(numbers[age].Item2, turn);
                    } else
                    {
                        numbers.Add(age, new Tuple<int, int>(-1, turn));
                    }
                    last = age;
                }
            }

            Console.WriteLine($"Puzzle 2 solution: {last}");
        }

        public void ReadInput()
        {
            var nums = "14,1,17,0,3,20".Split(',');
            spokenNumbers.AddRange(nums.Select(n => int.Parse(n)));
            for (int i = 0; i < nums.Length; i++)
            {
                numbers.Add(int.Parse(nums[i]), new Tuple<int, int>(-1, i + 1));
            }
        }
    }
}