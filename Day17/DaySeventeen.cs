using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

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

            // Run 7 cycles.
            for (var cycle = 1; cycle < 7; cycle++)
            {
                Console.WriteLine($"Cycle {cycle} starting...");
                var start = DateTime.UtcNow;
                // Expand grid with an extra inactives on all sides.
                var newSize = grid[0].Count + 2;
                var newGrid = new List<List<char[]>>(grid.Count + 2);
                var lower = new List<char[]>(newSize);
                var middle = new List<List<char[]>>(grid.Count);
                var upper = new List<char[]>(newSize);

                // Add inactives to either side of regular grid.
                for (var layer = 0; layer < grid.Count; layer++)
                {
                    var newLayer = new List<char[]>();
                    foreach (var row in grid[layer])
                    {
                        newLayer.Add(("." + string.Join("", row) + ".").ToCharArray());
                    }
                    middle.Add(newLayer);
                }

                // Fill either new side layer with empty values.
                for (var i = 0; i < newGrid.Capacity; i++)
                {
                    var c = new char[newSize];
                    Array.Fill(c, '.');
                    lower.Add(c);
                    upper.Add(c);
                }

                newGrid.Add(lower);
                newGrid.AddRange(middle);
                newGrid.Add(upper);

                grid = CloneGrid(newGrid);

                for (var z = 0; z < grid.Count; z++)
                {
                    for (var y = 0; y < grid[z].Count; y++)
                    {
                        for (var x = 0; x < grid[z][y].Length; x++)
                        {
                            // Console.WriteLine($"Checking ({z},{y},{x})");
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
                                } catch (Exception ex) when (ex is ArgumentOutOfRangeException || ex is IndexOutOfRangeException)
                                {
                                    // Console.WriteLine($"({neighbour.Item1},{neighbour.Item2},{neighbour.Item3}) does not exist.");
                                }
                            }

                            // Console.WriteLine($"{active} active neighbours for ({z},{y},{x}).");

                            // Active only stays active if 2 or 3 neighbours are active.
                            if (grid[z][y][x] == '#' && !(active == 2 || active == 3))
                            {
                                newGrid[z][y][x] = '.';    
                            }
                            // Inactive only becomes active if 3 neighbours are active.
                            else if (grid[z][y][x] == '.' && active == 3)
                            {
                                newGrid[z][y][x] = '#';
                            }
                        }
                    }
                }

                grid = newGrid;
                var a = 0;
                foreach (var layer in grid) {
                    foreach (var row in layer)
                    {
                        a += row.Count(x => x == '#');
                    }
                }
                Console.WriteLine($"{a} active after cycle {cycle}. ({(DateTime.UtcNow - start).TotalSeconds} seconds)");
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

        private List<List<char[]>> CloneGrid(List<List<char[]>> grid)
        {
            IFormatter formatter = new BinaryFormatter();
            using var stream = new MemoryStream();
            formatter.Serialize(stream, grid);
            stream.Seek(0, SeekOrigin.Begin);
            return (List<List<char[]>>)formatter.Deserialize(stream);
        }
    }
}