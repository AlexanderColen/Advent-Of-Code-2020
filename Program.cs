using AdventOfCode2020.Day01;
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