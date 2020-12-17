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
        private readonly HashSet<int> faultyTicketIndeces = new HashSet<int>();

        public DaySixteen()
        {
            ReadInput();
            Puzzle1();
            Puzzle2();
        }

        public void Puzzle1()
        {
            var solution = 0;

            for (var ticketIndex = 0; ticketIndex < otherTickets.Count; ticketIndex++)
            {
                foreach (var i in otherTickets[ticketIndex])
                {
                    for (var j = 0; j < fields.Count; j++)
                    {
                        if (fields[j].IsValid(i))
                        {
                            break;
                        } else if (j == fields.Count - 1)
                        {
                            solution += i;
                            faultyTicketIndeces.Add(ticketIndex);
                        } 
                    }
                }
            }

            Console.WriteLine($"Puzzle 1 solution: {solution}");
        }

        public void Puzzle2()
        {
            long solution = 1;
            
            // Find all possible fields for a ticket value.
            for (var i = 0; i < otherTickets.Count; i++)
            {
                // Ignore faulty tickets.
                if (!faultyTicketIndeces.Contains(i))
                {
                    foreach (var field in fields)
                    {
                        var possible = new List<int>();
                        for (var j = 0; j < otherTickets[i].Length; j++)
                        {
                            if (field.IsValid(otherTickets[i][j]))
                            {
                                possible.Add(j);
                            }
                        }
                        field.Possibles.Add(possible);
                        field.Intersection = field.Possibles.Skip(1).Aggregate(new HashSet<int>(field.Possibles.First()),(h, e) => { h.IntersectWith(e); return h; });
                    }
                }
            }

            // Determine field order and calculate solution.
            var order = new int[fields.Count];
            Array.Fill(order, -1);
            
            while (order.Where(x => x == -1).Count() > 0)
            {
                for (var i = 0; i < fields.Count; i++)
                {
                    if (fields[i].Intersection.Count == 1 && order.Contains(fields[i].Intersection.First()))
                    {
                        continue;
                    }

                    if (fields[i].Intersection.Count != 1)
                    {
                        // Remove used possibilities.
                        foreach (var o in order)
                        {
                            fields[i].Intersection.Remove(o);
                        }
                    }

                    // Only one possibility, so note it down.
                    if (fields[i].Intersection.Count == 1)
                    {
                        order[i] = fields[i].Intersection.First();
                        if (fields[i].Field.StartsWith("departure"))
                        {
                            solution *= myTicket[order[i]];
                        }
                        break;
                    }
                }
            }

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
                    var ranges = parts[1].Split(" or ");
                    var range1 = ranges[0].Split('-');
                    var range2 = ranges[1].Split('-');

                    var field = new TicketField()
                    {
                        Field = parts[0],
                        Min1 = int.Parse(range1[0]),
                        Max1 = int.Parse(range1[1]),
                        Min2 = int.Parse(range2[0]),
                        Max2 = int.Parse(range2[1]),
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