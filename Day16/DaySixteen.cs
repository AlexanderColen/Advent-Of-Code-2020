using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day16
{
    public class DaySixteen : IDay
    {
        private readonly List<TicketField> fields = new List<TicketField>();
        private readonly List<int> myTicket = new List<int>();
        private readonly List<int[]> otherTickets = new List<int[]>();

        public DaySixteen()
        {
            ReadInput();
            Puzzle1();
            Puzzle2();
        }

        public void Puzzle1()
        {
            var solution = 0;

            foreach (var ticket in otherTickets)
            {
                foreach (var i in ticket)
                {
                    for (var j = 0; j < fields.Count; j++)
                    {
                        if (fields[j].IsValid(i))
                        {
                            break;
                        } else if (j == fields.Count - 1)
                        {
                            solution += i;
                        } 
                    }
                }
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
            using StreamReader sr = new StreamReader(@"Day16/input.txt");
            string line;
            var handling = "fields";
            while ((line = sr.ReadLine()) != null)
            {
                if (line == "")
                {
                    continue;
                }

                // Change status.
                if (line.Equals("your ticket:"))
                {
                    handling = "mine";
                    continue;
                } else if (line.Equals("nearby tickets:"))
                {
                    handling = "others";
                    continue;
                }

                if (handling.Equals("fields"))
                {
                    var parts = line.Split(": ");
                    var ranges = parts[1].Replace(" or ", " ").Split(' ');
                    var range1 = ranges[0].Split('-');
                    var range2 = ranges[1].Split('-');

                    var field = new TicketField()
                    {
                        field = parts[0],
                        min1 = int.Parse(range1[0]),
                        max1 = int.Parse(range1[1]),
                        min2 = int.Parse(range2[0]),
                        max2 = int.Parse(range2[1]),
                    };
                    fields.Add(field);
                } else if (handling.Equals("mine"))
                {
                    myTicket.AddRange(line.Split(',').Select(x => int.Parse(x)));
                } else if (handling.Equals("others"))
                {
                    otherTickets.Add(line.Split(',').Select(x => int.Parse(x)).ToArray());
                }
            }
        }
    }
}