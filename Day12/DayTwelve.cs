using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020.Day12
{
    public class DayTwelve : IDay
    {
        private readonly List<string> instructions = new List<string>();

        public DayTwelve()
        {
            ReadInput();
            Puzzle1();
            Puzzle2();
        }

        public void Puzzle1()
        {
            var x = 0;
            var y = 0;
            // Start east.
            var rotation = 90;

            foreach (var instruction in instructions)
            {
                // Rotate right. (+degrees)
                if (instruction.StartsWith("R"))
                {
                    rotation += int.Parse(instruction.Replace("R", ""));
                }
                // Rotate left. (-degrees)
                else if (instruction.StartsWith("L"))
                {
                    rotation -= int.Parse(instruction.Replace("L", ""));
                }
                // Move north. (+y)
                else if (instruction.StartsWith("N"))
                {
                    y += int.Parse(instruction.Replace("N", ""));
                }
                // Move south. (-y)
                else if (instruction.StartsWith("S"))
                {
                    y -= int.Parse(instruction.Replace("S", ""));
                }
                // Move east. (+x)
                else if (instruction.StartsWith("E"))
                {
                    x += int.Parse(instruction.Replace("E", ""));
                }
                // Move west. (-x)
                else if (instruction.StartsWith("W"))
                {
                    x -= int.Parse(instruction.Replace("W", ""));
                }
                // Move forward in direction.
                else if (instruction.StartsWith("F"))
                {
                    var movement = int.Parse(instruction.Replace("F", ""));
                    // Facing north.
                    if (rotation % 360 == 0)
                    {
                        y += movement;
                    }
                    // Facing east.
                    else if (rotation % 360 == 90 || rotation % 360 == -270)
                    {
                        x += movement;
                    }
                    // Facing south.
                    else if (rotation % 360 == 180 || rotation % 360 == -180)
                    {
                        y -= movement;
                    }
                    // Facing west.
                    else if (rotation % 360 == 270 || rotation % 360 == -90)
                    {
                        x -= movement;
                    }
                }
            }

            Console.WriteLine($"Puzzle 1 solution: {Math.Abs(x) + Math.Abs(y)}");
        }

        public void Puzzle2()
        {
            var waypointX = 10;
            var waypointY = 1;
            var boatX = 0;
            var boatY = 0;

            foreach (var instruction in instructions)
            {
                // Rotate WAYPOINT right. (+degrees)
                if (instruction.StartsWith("R"))
                {
                    for (int i = 0; i < int.Parse(instruction.Replace("R", "")) / 90; i++)
                    {
                        var newY = -waypointX;
                        var newX = waypointY;
                        waypointX = newX;
                        waypointY = newY;
                    }
                }
                // Rotate WAYPOINT left. (-degrees)
                else if (instruction.StartsWith("L"))
                {
                    for (int i = 0; i < int.Parse(instruction.Replace("L", "")) / 90; i++)
                    {
                        var newY = waypointX;
                        var newX = -waypointY;
                        waypointX = newX;
                        waypointY = newY;
                    }
                }
                // Move WAYPOINT north. (+y)
                else if (instruction.StartsWith("N"))
                {
                    waypointY += int.Parse(instruction.Replace("N", ""));
                }
                // Move WAYPOINT south. (-y)
                else if (instruction.StartsWith("S"))
                {
                    waypointY -= int.Parse(instruction.Replace("S", ""));
                }
                // Move WAYPOINT east. (+x)
                else if (instruction.StartsWith("E"))
                {
                    waypointX += int.Parse(instruction.Replace("E", ""));
                }
                // Move WAYPOINT west. (-x)
                else if (instruction.StartsWith("W"))
                {
                    waypointX -= int.Parse(instruction.Replace("W", ""));
                }
                // Move BOAT forward towards waypoint X times.
                else if (instruction.StartsWith("F"))
                {
                    var movement = int.Parse(instruction.Replace("F", ""));
                    boatX += movement * waypointX;
                    boatY += movement * waypointY;
                }
            }

            Console.WriteLine($"Puzzle 2 solution: {Math.Abs(boatX) + Math.Abs(boatY)}");
        }

        public void ReadInput()
        {
            using StreamReader sr = new StreamReader(@"Day12/input.txt");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                instructions.Add(line);      
            }
        }
    }
}