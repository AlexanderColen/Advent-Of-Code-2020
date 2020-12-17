using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day17
{
    public class DaySeventeen : IDay
    {
        private List<List<char[]>> grid = new List<List<char[]>>();

        public DaySeventeen()
        {
            ReadInput();
            Puzzle1();
            Puzzle2();
        }

        public void Puzzle1()
        {
            var solution = 0;

            // Run 6 cycles.
            for (var cycle = 1; cycle < 7; cycle++)
            {
                Console.WriteLine($"Cycle {cycle} starting...");
                var newGrid = new List<List<char[]>>();
                for (var z = -1; z < grid.Count + 1; z++)
                {
                    var layer = new List<char[]>();
                    for (var y = -1; y < grid[0].Count + 1; y++)
                    {
                        var row = new List<char>();
                        for (var x = -1; x < grid[0][0].Length + 1; x++)
                        {
                            // Determine neighbours.
                            var neighbours = new List<Tuple<int, int, int>>()
                            {
                                // 1 layer lower.
                                new Tuple<int, int, int>(z - 1, y - 1, x - 1),
                                new Tuple<int, int, int>(z - 1, y - 1, x),
                                new Tuple<int, int, int>(z - 1, y - 1, x + 1),
                                new Tuple<int, int, int>(z - 1, y, x - 1),
                                new Tuple<int, int, int>(z - 1, y, x),
                                new Tuple<int, int, int>(z - 1, y, x + 1),
                                new Tuple<int, int, int>(z - 1, y + 1, x - 1),
                                new Tuple<int, int, int>(z - 1, y + 1, x),
                                new Tuple<int, int, int>(z - 1, y + 1, x + 1),
                                // Same layer.
                                new Tuple<int, int, int>(z, y - 1, x - 1),
                                new Tuple<int, int, int>(z, y - 1, x),
                                new Tuple<int, int, int>(z, y - 1, x + 1),
                                new Tuple<int, int, int>(z, y, x - 1),
                                new Tuple<int, int, int>(z, y, x + 1),
                                new Tuple<int, int, int>(z, y + 1, x - 1),
                                new Tuple<int, int, int>(z, y + 1, x),
                                new Tuple<int, int, int>(z, y + 1, x + 1),
                                // 1 layer higher.
                                new Tuple<int, int, int>(z + 1, y - 1, x - 1),
                                new Tuple<int, int, int>(z + 1, y - 1, x),
                                new Tuple<int, int, int>(z + 1, y - 1, x + 1),
                                new Tuple<int, int, int>(z + 1, y, x - 1),
                                new Tuple<int, int, int>(z + 1, y, x),
                                new Tuple<int, int, int>(z + 1, y, x + 1),
                                new Tuple<int, int, int>(z + 1, y + 1, x - 1),
                                new Tuple<int, int, int>(z + 1, y + 1, x),
                                new Tuple<int, int, int>(z + 1, y + 1, x + 1),
                            };

                            // Count active neighbours.
                            var active = 0;

                            foreach (var neighbour in neighbours)
                            {
                                try
                                {
                                    active += grid[neighbour.Item1][neighbour.Item2][neighbour.Item3] == '#' ? 1 : 0;
                                } catch (Exception ex) when (ex is ArgumentOutOfRangeException || ex is IndexOutOfRangeException) { }
                            }

                            try
                            {
                                // Active only stays active if 2 or 3 neighbours are active.
                                if (grid[z][y][x] == '#')
                                {
                                    row.Add(active == 2 || active == 3 ? '#' : '.');
                                }
                                // Inactive only becomes active if 3 neighbours are active.
                                else if (grid[z][y][x] == '.')
                                {
                                    row.Add(active == 3 ? '#' : '.');
                                }
                            } catch (Exception ex) when (ex is ArgumentOutOfRangeException || ex is IndexOutOfRangeException)
                            {
                                row.Add(active == 3 ? '#' : '.');
                            }                            
                        }
                        layer.Add(row.ToArray());
                    }
                    newGrid.Add(layer);
                }
                grid = newGrid;
            }

            foreach (var layer in grid) {
                foreach (var row in layer)
                {
                    solution += row.Count(x => x == '#');
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
            using StreamReader sr = new StreamReader(@"Day17/input.txt");
            string line;
            var z0 = new List<char[]>();
            while ((line = sr.ReadLine()) != null)
            {
                z0.Add(line.ToCharArray());
            }
            grid.Add(z0);
        }
    }
}