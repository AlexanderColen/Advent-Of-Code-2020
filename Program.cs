using AdventOfCode2020.Day01;
using AdventOfCode2020.Day02;
using AdventOfCode2020.Day03;
using AdventOfCode2020.Day04;
using AdventOfCode2020.Day05;
using AdventOfCode2020.Day06;
using System;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                switch (args[0])
                {
                    case "1":
                        new DayOne();
                        break;

                    case "2":
                        new DayTwo();
                        break;

                    case "3":
                        new DayThree();
                        break;

                    case "4":
                        new DayFour();
                        break;

                    case "5":
                        new DayFive();
                        break;

                    case "6":
                        new DaySix();
                        break;

                    default:
                        Console.Write("This Day's solution has not been implemented yet.");
                        break;
                }
            } else
            {
                Console.WriteLine("Please add a number parameter to indicate which Day's solution you would like to see.");
            }
        }
    }
}