using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day18
{
    public class DayEighteen : IDay
    {
        private readonly List<string> homework = new List<string>();

        public DayEighteen()
        {
            ReadInput();
            Puzzle1();
            Puzzle2();
        }

        public void Puzzle1()
        {
            ulong solution = 0;

            foreach (var expression in homework)
            {
                var ex = expression;
                // Keep looping until no more parentheses are there.
                while (ex.Contains('('))
                {
                    var re = new Regex(@"\([0-9+* ]+\)");
                    var match = re.Match(ex).ToString();
                    // Remove parentheses on both sides and evaluate.
                    var sub = match[1..^1];
                    var evaluated = EvaluateLeftToRight(sub);
                    // Replace with evaluated part.
                    var regex = new Regex(Regex.Escape(match));
                    ex = regex.Replace(ex, evaluated.ToString(), 1);
                }

                solution += EvaluateLeftToRight(ex);
            }

            Console.WriteLine($"Puzzle 1 solution: {solution}");
        }

        public void Puzzle2()
        {
            ulong solution = 0;

            foreach (var expression in homework)
            {
                var ex = expression;
                // Keep looping until no more parentheses are there.
                while (ex.Contains('('))
                {
                    var re = new Regex(@"\([0-9+* ]+\)");
                    var match = re.Match(ex).ToString();
                    // Remove parentheses on both sides and evaluate.
                    var sub = match[1..^1];
                    var evaluated = EvaluateAdditionBeforeMultiplication(sub);
                    // Replace with evaluated part.
                    var regex = new Regex(Regex.Escape(match));
                    ex = regex.Replace(ex, evaluated.ToString(), 1);
                }

                solution += EvaluateAdditionBeforeMultiplication(ex);
            }

            Console.WriteLine($"Puzzle 2 solution: {solution}");
        }

        public void ReadInput()
        {
            using StreamReader sr = new StreamReader(@"Day18/input.txt");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                homework.Add(line);
            }
        }

        private ulong EvaluateLeftToRight(string expression)
        {
            ulong result = 0;

            while (expression.Contains('+') || expression.Contains('*'))
            {
                // Match first expression.
                var re = new Regex(@"[0-9]+ [*+] [0-9]+");
                var match = re.Match(expression).ToString();
                // Match separate values.
                var re2 = new Regex(@"[0-9]+");
                var values = re2.Matches(match);
                ulong outcome = 0;
                if (match.Contains('+'))
                {
                    outcome = Convert.ToUInt64(values[0].ToString()) + Convert.ToUInt64(values[1].ToString());
                } else if (match.Contains('*'))
                {
                    outcome = Convert.ToUInt64(values[0].ToString()) * Convert.ToUInt64(values[1].ToString());
                }
                // Replace handled expression with outcome.
                var regex = new Regex(Regex.Escape(match));
                expression = regex.Replace(expression, outcome.ToString(), 1);
                result = outcome;
            }

            return result;
        }

        private ulong EvaluateAdditionBeforeMultiplication(string expression)
        {
            ulong result = 0;
            // Addition first.
            while (expression.Contains('+'))
            {
                // Match first expression.
                var r = new Regex(@"[0-9]+ \+ [0-9]+");
                var m = r.Match(expression).ToString();
                // Match separate values.
                var r2 = new Regex(@"[0-9]+");
                var v = r2.Matches(m);
                ulong o = Convert.ToUInt64(v[0].ToString()) + Convert.ToUInt64(v[1].ToString());
                // Replace handled expression with outcome.
                var r3 = new Regex(Regex.Escape(m));
                expression = r3.Replace(expression, o.ToString(), 1);
                result = o;
            }
            
            // Multiplication after.
            while (expression.Contains('*'))
            {
                // Match first expression.
                var r = new Regex(@"[0-9]+ \* [0-9]+");
                var m = r.Match(expression).ToString();
                // Match separate values.
                var r2 = new Regex(@"[0-9]+");
                var v = r2.Matches(m);
                ulong o = Convert.ToUInt64(v[0].ToString()) * Convert.ToUInt64(v[1].ToString());
                // Replace handled expression with outcome.
                var r3 = new Regex(Regex.Escape(m));
                expression = r3.Replace(expression, o.ToString(), 1);
                result = o;
            }

            return result;
        }
    }
}