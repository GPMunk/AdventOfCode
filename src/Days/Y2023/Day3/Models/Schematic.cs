namespace AdventOfCode.Y2023.Day3.Models
{
    public class Schematic
    {
        public List<Symbol> Symbols { get; set; } = new List<Symbol>();

        public List<PartNumber> PartNumbers { get; set; } = new List<PartNumber>();
    }
}
