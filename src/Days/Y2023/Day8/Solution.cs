using AdventOfCode.Infrastructure;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2023.Day8
{
    public class Solution : Solver
    {
        public override string SolvePartOne(string input)
        {
            return Solve(input, "AAA", "ZZZ");
        }

        public override string SolvePartTwo(string input)
        {
            return Solve(input, "A", "Z");
        }

        private new string Solve(string input, string startElement, string endElement)
        {
            var (instructions, maps) = Parse(input);

            var count = maps.Keys
                .Where(w => w.EndsWith(startElement))
                .Select(w => StepsToEndElement(w, endElement, instructions, maps))
                .Aggregate(1L, Lcm);

            return count.ToString();
        }

        private long StepsToEndElement(string current, string endElement, string instructions, Dictionary<string, (string left, string right)> maps)
        {
            var i = 0;
            while (!current.EndsWith(endElement))
            {
                var dir = instructions[i % instructions.Length];
                current = dir == 'L' ? maps[current].left : maps[current].right;
                i++;
            }
            return i;
        }

        long Lcm(long a, long b) => a * b / Gcd(a, b);
        long Gcd(long a, long b) => b == 0 ? a : Gcd(b, a % b);

        private (string instructions, Dictionary<string, (string left, string right)> maps) Parse(string input)
        {
            var rx = "[A-Z0-9]+";
            var blocks = input.Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries);
            var instructions = blocks[0];
            var maps = blocks[1].Split("\r\n")
                .Select(line => Regex.Matches(line, rx))
                .ToDictionary(m => m[0].Value, m => (m[1].Value, m[2].Value));

            return (instructions, maps);
        }
    }
}
