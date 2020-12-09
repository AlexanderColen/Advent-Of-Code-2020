using System;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                Console.WriteLine($"Day {args[0]}");
                switch (args[0])
                {
                    case "1":
                        new Day01.DayOne();
                        break;

                    case "2":
                        new Day02.DayTwo();
                        break;

                    case "3":
                        new Day03.DayThree();
                        break;

                    case "4":
                        new Day04.DayFour();
                        break;

                    case "5":
                        new Day05.DayFive();
                        break;

                    case "6":
                        new Day06.DaySix();
                        break;

                    case "7":
                        new Day07.DaySeven();
                        break;

                    case "8":
                        new Day08.DayEight();
                        break;

                    case "9":
                        new Day09.DayNine();
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