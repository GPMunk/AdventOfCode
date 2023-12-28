using AdventOfCode.Infrastructure;

namespace AdventOfCode.Y2023.Day1
{
    public class Solution : Solver
    {
        IParser<Calibration> Parser = new CalibrationParser();
        public IEnumerable<Calibration> Calibrations { get; set; }

        public override string SolvePartOne(string input)
        {
            var calibrations = GetCalibrations(input);
            var value = calibrations.Sum(x => x.Value);
            return value.ToString();
        }

        public override string SolvePartTwo(string input)
        {
            var calibrations = GetCalibrations(input);
            var value = calibrations.Sum(x => x.SpelledValue);
            return value.ToString();
        }

        private IEnumerable<Calibration> GetCalibrations(string input)
        {
            if (Calibrations == null)
            {
                Calibrations = Parser.Parse(input);
            }
            return Calibrations;
        }

        //private int Solve(IEnumerable<Calibration> input, bool convertWords)
        //{
        //    var value = 0;
        //    foreach (var calibration in input)
        //    {
        //        var modLine = calibration.Line;

        //        if (convertWords)
        //        {
        //            foreach (var map in numberMapping)
        //            {
        //                modLine = modLine.Replace(map.Key, map.Value);
        //            }
        //        }

        //        var regex = new Regex("\\d");
        //        var matches = regex.Matches(modLine);
        //        if (!matches.Any())
        //            continue;

        //        var test = int.Parse(matches[0].Value) * 10;
        //        var test2 = int.Parse(matches[matches.Count - 1].Value);

        //        value += test + test2;
        //    }

        //    return value;
        //}
    }
}
