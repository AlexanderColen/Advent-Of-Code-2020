using System.Collections.Generic;

namespace AdventOfCode2020.Day16
{
    public class TicketField
    {
        public string Field { get; set; }
        public int Min1 { get; set; }
        public int Max1 { get; set; }
        public int Min2 { get; set; }
        public int Max2 { get; set; }
        public List<List<int>> Possibles { get; set; }
        public HashSet<int> Intersection { get; set; }

        public TicketField()
        {
            Possibles = new List<List<int>>();
            Intersection = new HashSet<int>();
        }

        public bool IsValid(int value)
        {
            return (value >= Min1 && value <= Max1) || (value >= Min2 && value <= Max2);
        }
    }
}