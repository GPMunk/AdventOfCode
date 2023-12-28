using AdventOfCode.Infrastructure;
using AdventOfCode.Y2023.Day3.Models;

namespace AdventOfCode.Y2023.Day3
{
    public class Solution : Solver
    {
        public override SolveResult SolvePartOne(string input)
        {
            var schematic = SchematicParser.Parse(input);
            var partNrs = GetPartNumbersAdjacentToSymbol(schematic);
            var sum = partNrs.Sum();

            return new SolveResult
            {
                Part = 1,
                Answer = sum.ToString()
            };
        }

        public override SolveResult SolvePartTwo(string input)
        {
            var schematic = SchematicParser.Parse(input);
            var gearRatioSum = GetGearRatioSum(schematic);

            return new SolveResult
            {
                Part = 1,
                Answer = gearRatioSum.ToString()
            };
        }

        public List<int> GetPartNumbersAdjacentToSymbol(Schematic schematic)
        {
            var adjacentPartNumbers = new List<int>();
            foreach (var partNumber in schematic.PartNumbers)
            {
                var symbolsRowAbove = schematic.Symbols.Where(x =>
                        x.Row == partNumber.Row - 1 &&
                        x.Index >= partNumber.Index - 1 &&
                        x.Index <= partNumber.Index + partNumber.StringValue.Length
                    ).ToList();

                var symbolsRow = schematic.Symbols.Where(x =>
                        x.Row == partNumber.Row &&
                        (x.Index == partNumber.Index - 1 ||
                        x.Index == partNumber.Index + partNumber.StringValue.Length)
                    ).ToList();

                var symbolsRowBelow = schematic.Symbols.Where(x =>
                        x.Row == partNumber.Row + 1 &&
                        x.Index >= partNumber.Index - 1 &&
                        x.Index <= partNumber.Index + partNumber.StringValue.Length
                    ).ToList();

                var symbols = symbolsRowAbove.Concat(symbolsRow).Concat(symbolsRowBelow);

                if (symbols.Any())
                {
                    adjacentPartNumbers.Add(partNumber.Value);
                }
            }

            return adjacentPartNumbers;
        }

        public int GetGearRatioSum(Schematic schematic)
        {
            var sum = 0;

            var gearSymbols = schematic.Symbols.Where(x => x.Character == '*').ToList();
            foreach (var gearSymbol in gearSymbols)
            {
                var partNrsAbove = schematic.PartNumbers.Where(x =>
                        x.Row == gearSymbol.Row - 1 &&
                        x.Indices.Contains(gearSymbol.Index)
                    ).ToList();

                var partNrsRow = schematic.PartNumbers.Where(x =>
                        x.Row == gearSymbol.Row &&
                        (x.Index + x.StringValue.Length - 1 == gearSymbol.Index - 1 ||
                        x.Index == gearSymbol.Index + 1)
                    ).ToList();

                var partNrsBelow = schematic.PartNumbers.Where(x =>
                        x.Row == gearSymbol.Row + 1 &&
                        x.Indices.Contains(gearSymbol.Index)
                    ).ToList();

                var partNrs = partNrsAbove.Concat(partNrsRow).Concat(partNrsBelow).ToList();

                if (partNrs.Count == 2)
                    sum += partNrs[0].Value * partNrs[1].Value;
            }

            return sum;
        }
    }
}
