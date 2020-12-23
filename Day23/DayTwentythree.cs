using System;
using System.Linq;

namespace AdventOfCode2020.Day23
{
    public class DayTwentythree : IDay
    {
        private string cups;
        
        public DayTwentythree()
        {
            ReadInput();
            Puzzle1();
            Puzzle2();
        }

        public void Puzzle1()
        {
            var current = 0;
            var moves = 100;

            // Play X moves.
            for (int i = 0; i < moves; i++)
            {
                // Check the label of the current cup.
                var label = int.Parse(cups[current].ToString());

                // Pick up the three cups after the current cup.
                var threeCups = RemoveThreeCupsAfter(current);

                // Find value with label 1 less than current.
                // Value must be in cups, if not just keep going down.
                // Loop back to highest value otherwise.
                var next = label - 1;
                while (!cups.Contains((next).ToString()))
                {
                    next--;
                    if (next <= 0)
                    {
                        next = cups.Select(x => int.Parse(x.ToString())).Max();
                    }
                }

                // Put three selected cups right after the current cup.
                var destination = cups.IndexOf(next.ToString());
                cups = cups.Substring(0, destination + 1) + threeCups + cups[(destination + 1)..];

                // Rotate cups to keep indexing properly working.
                while (int.Parse(cups[current].ToString()) != label)
                {
                    cups = cups.Replace(cups[0].ToString(), "") + cups[0];
                }

                // Select next current cup.
                current++;
                if (current > cups.Length - 1)
                {
                    current = 0;
                }
            }

            RotateUntilOneIsTrailing();

            Console.WriteLine($"Puzzle 1 solution: {cups[0..^1]}");
        }

        public void Puzzle2()
        {
            var solution = 0;

            Console.WriteLine($"Puzzle 2 solution: {solution}");
        }

        public void ReadInput()
        {
            var input = "253149867";
            cups = input;
        }

        // Loops to start after last index.
        private string RemoveThreeCupsAfter(int index)
        {
            index++;
            var threeCups = "";
            while (threeCups.Length != 3)
            {
                try
                {
                    threeCups += cups.Substring(index, 1);
                    index++;
                } catch (ArgumentOutOfRangeException)
                {
                    index = 0;
                }
            }
            
            // Remove instances of the three cups.
            foreach (var c in threeCups)
            {
                cups = cups.Replace(c.ToString(), "");
            }

            return threeCups;
        }

        private void RotateUntilOneIsTrailing()
        {
            while ('1' != cups[^1])
            {
                cups = cups.Replace(cups[0].ToString(), "") + cups[0];
            }
        }
    }
}