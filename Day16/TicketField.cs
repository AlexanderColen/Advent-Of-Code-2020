namespace AdventOfCode2020.Day16
{
    public class TicketField
    {
        public string field { get; set; }
        public int min1 { get; set; }
        public int max1 { get; set; }
        public int min2 { get; set; }
        public int max2 { get; set; }

        public bool IsValid(int value)
        {
            return (value >= min1 && value <= max1) || (value >= min2 && value <= max2);
        }
    }
}