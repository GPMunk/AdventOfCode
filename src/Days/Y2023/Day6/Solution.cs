using AdventOfCode.Infrastructure;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2023.Day6
{
    public class Solution : Solver
    {
        public override string SolvePartOne(string input)
        {
            var races = ParseInput(input);
            var margin = races.Aggregate(1L, (x, y) => x * y.NumberOfWays);
            return margin.ToString();
        }

        public override string SolvePartTwo(string input)
        {
            var races = ParseInput(input, x => x.Replace(" ", "")).ToArray();


            return races[0].NumberOfWays.ToString();
        }

        private IEnumerable<Race> ParseInput(string input)
        {
            return ParseInput(input, x => x);
        }

        private IEnumerable<Race> ParseInput(string input, Func<string, string> f)
        {
            var rx = "\\d+";
            var inputBlocks = input.Split("\r\n");

            var times = Regex.Matches(f(inputBlocks[0]), rx).Select(x => long.Parse(x.Value)).ToArray();
            var distances = Regex.Matches(f(inputBlocks[1]), rx).Select(x => long.Parse(x.Value)).ToArray();
            for (int i = 0; i < times.Length; i++)
            {
                yield return new Race
                {
                    Time = times[i],
                    RecordDistance = distances[i]
                };
            }
        }
    }

    class Race
    {
        public long Time { get; set; }

        public long RecordDistance { get; set; }

        public long NumberOfWays
        {
            get
            {
                // Eigen oplossing:
                //var distances = new List<long>();
                //for (long i = 0;i <= Time; i++)
                //{
                //    distances.Add((Time - i) * i);
                //}

                //return distances.Count(x => x > RecordDistance);

                // Oplossing van GitHub:
                var a = -1;
                var b = Time;
                var c = -RecordDistance;

                var d = Math.Sqrt(b * b - 4 * a * c);
                var x1 = (-b - d) / (2 * a);
                var x2 = (-b + d) / (2 * a);
                var min = Math.Min(x1, x2);
                var max = Math.Max(x1, x2);

                var maxX = (int)Math.Ceiling(max) - 1;
                var minX = (int)Math.Floor(min) + 1;
                return maxX - minX + 1;
            }
        }
    }
}
