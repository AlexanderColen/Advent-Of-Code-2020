using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day22
{
    public class DayTwentytwo : IDay
    {
        private readonly Queue<int> Deck1 = new Queue<int>();
        private readonly Queue<int> Deck2 = new Queue<int>();

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
                var c1 = Deck1.Dequeue();
                var c2 = Deck2.Dequeue();

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
                solution += winner.Dequeue() * i;
            }


            Console.WriteLine($"Puzzle 1 solution: {solution}");
        }

        public void Puzzle2()
        {
            var solution = 0;

            // Reset the deck.
            ReadInput();

            var winner = PlayRecursiveCombat(Deck1, Deck2) == 1 ? Deck1 : Deck2;

            for (var i = winner.Count; i > 0; i--)
            {
                solution += winner.Dequeue() * i;
            }

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

        private int PlayRecursiveCombat(Queue<int> q1, Queue<int> q2)
        {
            var configurations1 = new List<string>();
            var configurations2 = new List<string>();

            while (q1.Count != 0 && q2.Count != 0)
            {
                var merged1 = string.Join("-", q1);
                var merged2 = string.Join("-", q2);
                // If a previous configuration was equal to the current one, player 1 wins the (sub)game.
                if (configurations1.Contains(merged1) || configurations2.Contains(merged2))
                {
                    return 1;
                } else
                {
                    configurations1.Add(merged1);
                    configurations2.Add(merged2);
                }

                var c1 = q1.Dequeue();
                var c2 = q2.Dequeue();
                int winner;

                // Launch a sub-game if the players both have at least the amount of cards left equal to the value they drew.
                if (q1.Count >= c1 && q2.Count >= c2)
                {
                    winner = PlayRecursiveCombat(new Queue<int>(q1.Take(c1)), new Queue<int>(q2.Take(c2)));
                }
                // Higher card wins the round since no sub-game was launched.
                else
                {
                    winner = c1 > c2 ? 1 : 2;
                }

                if (winner == 1)
                {
                    q1.Enqueue(c1);
                    q1.Enqueue(c2);
                } else
                {
                    q2.Enqueue(c2);
                    q2.Enqueue(c1);
                }
            }

            return q2.Count == 0 ? 1 : 2;
        }
    }
}