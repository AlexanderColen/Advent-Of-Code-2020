using System;
using System.Collections.Generic;

namespace AdventOfCode2020.Day07
{
    public class Bag
    {
        public string Colour { get; set; }
        public bool HasDirectShiny { get; set; }
        public List<Tuple<Bag, int>> Contents { get; set; }

        public Bag()
        {
            Contents = new List<Tuple<Bag, int>>();
        }
    }
}