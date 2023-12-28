using System.Text.RegularExpressions;
using AdventOfCode.Y2023.Day3.Models;

namespace AdventOfCode.Y2023.Day3
{
    public static class SchematicParser
    {
        public static Schematic Parse(string input)
        {
            return Parse(input.Split("\r\n"));
        }

        public static Schematic Parse(string[] lines)
        {
            var schematic = new Schematic();

            var index = 0;
            foreach (var line in lines)
            {
                (List<Symbol> rowSymbols, List<PartNumber> rowPartNumbers) = ParseLine(index, line);
                schematic.Symbols.AddRange(rowSymbols);
                schematic.PartNumbers.AddRange(rowPartNumbers);
                index++;
            }

            return schematic;
        }

        private static (List<Symbol> rowSymbols, List<PartNumber> rowPartNumbers) ParseLine(int row, string line)
        {
            var symbolMatches = Regex.Matches(line, "[^a-zA-Z0-9_.]");
            var rowSymbols = symbolMatches.Select(x => new Symbol
            {
                Row = row,
                Index = x.Index,
                Character = char.Parse(x.Value),
            }).ToList();

            var partMatches = Regex.Matches(line, "\\d+");
            var rowPartNumbers = partMatches.Select(x => new PartNumber
            {
                Row = row,
                Index = x.Index,
                StringValue = x.Value
            }).ToList();

            return (rowSymbols, rowPartNumbers);
        }
    }
}
