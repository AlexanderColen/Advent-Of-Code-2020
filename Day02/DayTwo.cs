using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day02
{
    public class DayTwo : IDay
    {
        private List<Input> inputs;

        public DayTwo()
        {
            ReadInput();
            Puzzle1();
            Puzzle2();
        }

        public void Puzzle1()
        {
            int solution = 0;

            foreach (var i in inputs) {
                if (i.Password.Where(x => (x == i.Character)).Count() >= i.Min && i.Password.Where(x => (x == i.Character)).Count() <= i.Max)
                {
                    solution++;
                }
            }

            Console.WriteLine($"Puzzle 1 solution: {solution}");
        }

        public void Puzzle2()
        {
            int solution = 0;

            foreach (var i in inputs)
            {
                if ((i.Password[i.Min - 1] == i.Character && i.Password[i.Max - 1] != i.Character) || (i.Password[i.Min - 1] != i.Character && i.Password[i.Max - 1] == i.Character))
                {
                    solution++;
                }
            }

            Console.WriteLine($"Puzzle 2 solution: {solution}");
        }

        public void ReadInput()
        {
            using StreamReader sr = new StreamReader(@"Day02/input.txt");
            inputs = new List<Input>();
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                Input i = new Input();

                string[] parts = line.Split(' ');
                i.Min = int.Parse(parts[0].Split('-')[0]);
                i.Max = int.Parse(parts[0].Split('-')[1]);
                i.Character = parts[1][0];
                i.Password = parts[2];

                inputs.Add(i);
            }
        }
    }
}