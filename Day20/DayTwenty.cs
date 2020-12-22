using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day20
{
    public class DayTwenty : IDay
    {
        private readonly Dictionary<int, List<char[]>> pieces = new Dictionary<int, List<char[]>>();

        public DayTwenty()
        {
            ReadInput();
            Puzzle1();
            Puzzle2();
        }

        public void Puzzle1()
        {
            var puzzle = SolvePuzzle(new List<List<int>>(10), pieces);
            var lastIndex = puzzle.Capacity - 1;

            //Console.WriteLine($"Puzzle 1 solution: {puzzle[0][0] * puzzle[0][lastIndex] * puzzle[lastIndex][0] * puzzle[lastIndex][lastIndex]}");
        }

        public void Puzzle2()
        {
            var solution = 0;

            Console.WriteLine($"Puzzle 2 solution: {solution}");
        }

        public void ReadInput()
        {
            using StreamReader sr = new StreamReader(@"Day20/input.txt");
            string line;
            List<char[]> piece = null;
            var tile = 0;
            while ((line = sr.ReadLine()) != null)
            {
                if (line == "")
                {
                    pieces.Add(tile, piece);
                    continue;
                }

                if (line.StartsWith("Tile"))
                {
                    tile = int.Parse(line[5..^1]);
                    piece = new List<char[]>();
                } else
                {
                    piece.Add(line.ToCharArray());
                }
            }
        }

        private List<char[]> MirrorTile(List<char[]> tile)
        {
            for (var i = 0; i < tile.Count; i++)
            {
                tile[i] = tile[i].Reverse().ToArray();
            }

            return tile;
        }

        private List<char[]> RotateTile(List<char[]> tile)
        {
            var newTile = new List<char[]>()
            {
                "----------".ToCharArray(),
                "----------".ToCharArray(),
                "----------".ToCharArray(),
                "----------".ToCharArray(),
                "----------".ToCharArray(),
                "----------".ToCharArray(),
                "----------".ToCharArray(),
                "----------".ToCharArray(),
                "----------".ToCharArray(),
                "----------".ToCharArray(),
            };

            for (int y = 0; y < tile.Count; y++)
            {
                for (int x = 0; x < tile[y].Length; x++)
                {
                    newTile[y][x] = tile[tile.Count - x - 1][y];
                }
            }

            return newTile;
        }

        private List<List<char[]>> GetVariations(List<char[]> tile)
        {
            var variations = new List<List<char[]>>() { tile };

            for (int i = 0; i < 3; i++) {
                tile = RotateTile(tile);
                variations.Add(tile);
            }

            tile = MirrorTile(tile);
            variations.Add(tile);

            for (int i = 0; i < 3; i++) {
                tile = RotateTile(tile);
                variations.Add(tile);
            }

            return variations;
        }

        private bool MatchEdge(List<char[]> tileA, List<char[]> tileB, char directionA, char directionB)
        {
            char[] rowA = null;
            char[] rowB = null;
            Console.WriteLine($"{directionA} - {directionB}");
            Console.WriteLine("Tile A");
            foreach (var row in tileA)
            {
                Console.WriteLine(string.Join("", row));
            }
            Console.WriteLine("Tile B");
            foreach (var row in tileB)
            {
                Console.WriteLine(string.Join("", row));
            }

            // Fetch edge that needs to match from tileA
            // Top of tileA
            if (directionA == 'N')
            {
                rowA = tileA[0];
            }
            // Right side of tileA
            else if (directionA == 'E')
            {
                tileA = RotateTile(tileA);
                tileA = RotateTile(tileA);
                tileA = RotateTile(tileA);
                rowA = tileA[0];
            }
            // Bottom of tileA
            else if (directionA == 'S')
            {
                rowA = tileA[^1];
            }
            // Left side of tileA
            else if (directionA == 'W')
            {
                tileA = RotateTile(tileA);
                rowA = tileA[0];
            }
            
            // Fetch edge that needs to match from tileB
            // Top of tileB
            if (directionB == 'N')
            {
                rowB = tileB[0];
            }
            // Right side of tileB
            else if (directionB == 'E')
            {
                tileB = RotateTile(tileB);
                tileB = RotateTile(tileB);
                tileB = RotateTile(tileB);
                rowB = tileB[0];
            }
            // Bottom of tileB
            else if (directionB == 'S')
            {
                rowB = tileB[^1];
            }
            // Left side of tileB
            else if (directionB == 'W')
            {
                tileB = RotateTile(tileB);
                rowB = tileB[0];
            }
            
            Console.WriteLine(string.Join("", rowA));
            Console.WriteLine(string.Join("", rowB));

            // Match edges.
            for (var i = 0; i < rowA.Length; i++)
            {
                if (rowA[i] != rowB[i])
                {
                    return false;
                }
            }
            return true;
        }

        private List<List<int>> SolvePuzzle(List<List<int>> solution, Dictionary<int, List<char[]>> leftovers)
        {
            foreach (var kv in leftovers)
            {
                // Skip duplicates.
                if (solution.Any(p => p.Contains(kv.Key)))
                {
                    continue;
                }


            }

            return solution;
        }
    }
}