using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day11
{
    public class DayEleven : IDay
    {
        private readonly List<string> seating = new List<string>();

        public DayEleven()
        {
            ReadInput();
            Puzzle1();
            Puzzle2();
        }

        public void Puzzle1()
        {
            var solution = 0;
            var arrangement = seating;
            var occupying = false;
            var round = 0;
            while (true)
            {
                // Increment and inverse.
                round++;
                occupying = !occupying;

                arrangement = ChangeSeatsOldRules(arrangement, occupying);

                // Count occupied;
                var count = 0;
                foreach (var row in arrangement)
                {
                    count += row.Count(x => x == '#');
                }
                if (count == solution)
                {
                    break;
                }
                solution = count;
            }

            Console.WriteLine($"Puzzle 1 solution: {solution}");
        }

        public void Puzzle2()
        {
            var solution = 0;
            var arrangement = seating;
            var occupying = false;
            var round = 0;
            while (true)
            {
                // Increment and inverse.
                round++;
                occupying = !occupying;

                arrangement = ChangeSeatsNewRules(arrangement, occupying);

                // Count occupied;
                var count = 0;
                foreach (var row in arrangement)
                {
                    count += row.Count(x => x == '#');
                }
                if (count == solution)
                {
                    break;
                }
                solution = count;
            }

            Console.WriteLine($"Puzzle 2 solution: {solution}");
        }

        public void ReadInput()
        {
            using StreamReader sr = new StreamReader(@"Day11/input.txt");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                seating.Add(line);                
            }
        }

        private List<string> ChangeSeatsOldRules(List<string> arrangement, bool occupying)
        {
            var changedArrangement = new List<string>();
            for (var y = 0; y < arrangement.Count; y++) {
                var newRow = "";
                for (var x = 0; x < arrangement[y].Length; x++)
                {
                    // Always skip dots.
                    if (arrangement[y][x] == '.')
                    {
                        newRow += '.';
                        continue;
                    }

                    // Skip L when not occupying.
                    if (!occupying && arrangement[y][x] == 'L')
                    {
                        newRow += 'L';
                        continue;
                    }

                    // Skip # when occupying.
                    if (occupying && arrangement[y][x] == '#')
                    {
                        newRow += '#';
                        continue;
                    }

                    // Define seats around the current seat.
                    var seatsAround = new List<Tuple<int, int>>()
                    {
                        new Tuple<int, int>(y - 1, x - 1),
                        new Tuple<int, int>(y - 1, x),
                        new Tuple<int, int>(y - 1, x + 1),
                        new Tuple<int, int>(y, x - 1),
                        new Tuple<int, int>(y, x + 1),
                        new Tuple<int, int>(y + 1, x - 1),
                        new Tuple<int, int>(y + 1, x),
                        new Tuple<int, int>(y + 1, x + 1)
                    };

                    var occupied = 0;
                    foreach (var seat in seatsAround)
                    {
                        // Skip invalid seats.
                        if (seat.Item1 < 0 || seat.Item2 < 0 || seat.Item1 >= arrangement.Count || seat.Item2 >= arrangement[y].Length)
                        {
                            continue;
                        }

                        occupied += arrangement[seat.Item1][seat.Item2] == '#' ? 1 : 0;
                    }

                    /*
                     * If occupying, a seat becomes empty with more than 4 occupied surrounding.
                     * Otherwise empty seats become occupied if 0 occupied surrounding.
                     */
                    if (occupying)
                    {
                        newRow += occupied > 0 ? 'L' : '#';
                    } else
                    {
                        newRow += occupied >= 4 ? 'L' : '#';
                    }
                }
                
                changedArrangement.Add(newRow);
            }

            return changedArrangement;
        }

        private List<string> ChangeSeatsNewRules(List<string> arrangement, bool occupying)
        {
            var changedArrangement = new List<string>();
            for (var y = 0; y < arrangement.Count; y++)
            {
                var newRow = "";
                for (var x = 0; x < arrangement[y].Length; x++)
                {
                    // Always skip dots.
                    if (arrangement[y][x] == '.')
                    {
                        newRow += '.';
                        continue;
                    }

                    // Skip L when not occupying.
                    if (!occupying && arrangement[y][x] == 'L')
                    {
                        newRow += 'L';
                        continue;
                    }

                    // Skip # when occupying.
                    if (occupying && arrangement[y][x] == '#')
                    {
                        newRow += '#';
                        continue;
                    }

                    var occupied = 0;
                    // Look diagonal left up. ( < x , < y )
                    var visY = y - 1;
                    var visX = x - 1;
                    while (visY > -1 && visX > -1)
                    {
                        if (arrangement[visY][visX] != '.')
                        {
                            occupied += arrangement[visY][visX] == '#' ? 1 : 0;
                            break;
                        }
                        visY--;
                        visX--;
                    }

                    // Look up. ( = x , < y )
                    for (visY = y - 1; visY > -1; visY--)
                    {
                        if (arrangement[visY][x] != '.')
                        {
                            occupied += arrangement[visY][x] == '#' ? 1 : 0;
                            break;
                        }
                    }

                    // Look diagonal right up. ( > x, < y )
                    visY = y - 1;
                    visX = x + 1;
                    while (visY > -1 && visX < arrangement[y].Length)
                    {
                        if (arrangement[visY][visX] != '.')
                        {
                            occupied += arrangement[visY][visX] == '#' ? 1 : 0;
                            break;
                        }
                        visY--;
                        visX++;
                    }

                    // Look right. ( > x , = y )
                    for (visX = x + 1; visX < arrangement[y].Length; visX++)
                    {
                        if (arrangement[y][visX] != '.')
                        {
                            occupied += arrangement[y][visX] == '#' ? 1 : 0;
                            break;
                        }
                    }

                    // Look diagonal right down. ( > x , > y )
                    visY = y + 1;
                    visX = x + 1;
                    while (visY < arrangement.Count && visX < arrangement[y].Length)
                    {
                        if (arrangement[visY][visX] != '.')
                        {
                            occupied += arrangement[visY][visX] == '#' ? 1 : 0;
                            break;
                        }
                        visY++;
                        visX++;
                    }

                    // Look down. ( = x , > y )
                    for (visY = y + 1; visY < arrangement.Count; visY++)
                    {
                        if (arrangement[visY][x] != '.')
                        {
                            occupied += arrangement[visY][x] == '#' ? 1 : 0;
                            break;
                        }
                    }

                    // Look diagonal left down. ( < x , > y )
                    visY = y + 1;
                    visX = x - 1;
                    while (visY < arrangement.Count && visX > -1)
                    {
                        if (arrangement[visY][visX] != '.')
                        {
                            occupied += arrangement[visY][visX] == '#' ? 1 : 0;
                            break;
                        }
                        visY++;
                        visX--;
                    }

                    // Look left. ( < x , = y)
                    for (visX = x - 1; visX > -1; visX--)
                    {
                        if (arrangement[y][visX] != '.')
                        {
                            occupied += arrangement[y][visX] == '#' ? 1 : 0;
                            break;
                        }
                    }

                    /*
                     * If occupying, a seat becomes empty with more than 5 occupied visible.
                     * Otherwise empty seats become occupied if 0 occupied visible.
                     */
                    if (occupying)
                    {
                        newRow += occupied > 0 ? 'L' : '#';
                    } else
                    {
                        newRow += occupied >= 5 ? 'L' : '#';
                    }
                }

                changedArrangement.Add(newRow);
            }
            return changedArrangement;
        }
    }
}