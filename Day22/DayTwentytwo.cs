using System;
using System.Collections;
using System.IO;

namespace AdventOfCode2020.Day22
{
    public class DayTwentytwo : IDay
    {
        private readonly Queue Deck1 = new Queue();
        private readonly Queue Deck2 = new Queue();

        public DayTwentytwo()
        {
            ReadInput();
            Puzzle1();
            Puzzle2();
        }

        public void Puzzle1()
        {
            var solution = 0;

            while (Deck1.Count != 0 && Deck2.Count != 0)
            {
                var c1 = int.Parse(Deck1.Dequeue().ToString());
                var c2 = int.Parse(Deck2.Dequeue().ToString());

                if (c1 > c2)
                {
                    Deck1.Enqueue(c1);
                    Deck1.Enqueue(c2);
                } else if (c2 > c1)
                {
                    Deck2.Enqueue(c2);
                    Deck2.Enqueue(c1);
                }
            }

            var winner = Deck1.Count == 0 ? Deck2 : Deck1;

            for (var i = winner.Count; i > 0; i--)
            {
                solution += int.Parse(winner.Dequeue().ToString()) * i;
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
            using StreamReader sr = new StreamReader(@"Day22/input.txt");
            string line;
            var player = 1;
            while ((line = sr.ReadLine()) != null)
            {
                if (line == "")
                {
                    continue;
                }

                if (line.StartsWith("Player"))
                {
                    player = int.Parse(line[^2].ToString());
                    continue;
                }

                if (player == 1)
                {
                    Deck1.Enqueue(int.Parse(line));
                } else if (player == 2)
                {
                    Deck2.Enqueue(int.Parse(line));
                }
            }
        }
    }
}