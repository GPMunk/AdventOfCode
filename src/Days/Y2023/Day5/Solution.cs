using AdventOfCode.Infrastructure;

namespace AdventOfCode.Y2023.Day5
{
    public class Solution : Solver
    {
        IEnumerable<Range> GetSeeds(string[] blocks)
        {
            var splitInput = blocks[0].Split(":", StringSplitOptions.RemoveEmptyEntries)[1];
            var seeds = FarmParser.GetSeeds(FarmParser.ParseLine(splitInput));
            return seeds;
        }
        IEnumerable<Range> GetRangeSeeds(string[] blocks)
        {
            var splitInput = blocks[0].Split(":", StringSplitOptions.RemoveEmptyEntries)[1];
            var seeds = FarmParser.GetRangeSeeds(FarmParser.ParseLine(splitInput));
            return seeds;
        }

        IEnumerable<Dictionary<Range, Range>> GetMaps(string[] blocks) => blocks.Skip(1).Select(FarmParser.ParseMap);

        public override SolveResult SolvePartOne(string input)
        {
            var blocks = input.Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries);
            var seeds = GetSeeds(blocks);
            var maps = GetMaps(blocks);

            var lowestNumber = maps.Aggregate(seeds, Project).Min(x => x.Start);

            return new SolveResult
            {
                Part = 1,
                Answer = lowestNumber.ToString()
            };
        }

        public override SolveResult SolvePartTwo(string input)
        {
            var blocks = input.Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries);
            var seeds = GetRangeSeeds(blocks);
            var maps = GetMaps(blocks);

            var lowestNumber = maps.Aggregate(seeds, Project).Min(x => x.Start);

            return new SolveResult
            {
                Part = 2,
                Answer = lowestNumber.ToString()
            };
        }

        public IEnumerable<Range> Project(IEnumerable<Range> inputRanges, Dictionary<Range, Range> map)
        {
            var input = new Queue<Range>(inputRanges);
            var output = new List<Range>();

            while (input.Any())
            {
                var range = input.Dequeue();
                // If no entry intersects our range -> just add it to the output. 
                // If an entry completely contains the range -> add after mapping.
                // Otherwise, some entry partly covers the range. In this case 'chop' 
                // the range into two halfs getting rid of the intersection. The new 
                // pieces are added back to the queue for further processing and will be 
                // ultimately consumed by the first two cases.
                var src = map.Keys.FirstOrDefault(src => Intersects(src, range));
                if (src == null)
                {
                    output.Add(range);
                }
                else if (src.Start <= range.Start && range.End <= src.End)
                {
                    var dst = map[src];
                    var shift = dst.Start - src.Start;
                    output.Add(new Range(range.Start + shift, range.End + shift));
                }
                else if (range.Start < src.Start)
                {
                    input.Enqueue(new Range(range.Start, src.Start - 1));
                    input.Enqueue(new Range(src.Start, range.End));
                }
                else
                {
                    input.Enqueue(new Range(range.Start, src.End));
                    input.Enqueue(new Range(src.End + 1, range.End));
                }
            }

            return output;
        }

        private bool Intersects(Range r1, Range r2)
        {
            return r1.Start <= r2.End && r2.Start <= r1.End;
        }

    }
}
