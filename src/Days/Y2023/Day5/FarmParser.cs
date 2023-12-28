using System.Text.RegularExpressions;

namespace AdventOfCode.Y2023.Day5
{
    public static class FarmParser
    {
        public static Dictionary<Range, Range> ParseMap(string input)
        {
            var dict = new Dictionary<Range, Range>();
            var lines = input.Split("\n").Skip(1);
            foreach (var line in lines)
            {
                var numbers = ParseLine(line).ToArray();
                dict.Add(
                    new Range(numbers[1], numbers[2] + numbers[1] - 1),
                    new Range(numbers[0], numbers[2] + numbers[0] - 1)
                    );
            }
            return dict;
        }

        public static IEnumerable<Range> GetRangeSeeds(IEnumerable<long> numbers) => numbers.Chunk(2).Select(x => new Range(x[0], x[0] + x[1] - 1));
        public static IEnumerable<Range> GetSeeds(IEnumerable<long> numbers) => numbers.Select(x => new Range(x, x));

        public static IEnumerable<long> ParseLine(string line)
        {
            return Regex.Matches(line, "\\d+").Select(x => long.Parse(x.Value));
        }
    }
}
