using AdventOfCode.Infrastructure;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2023.Day1
{
    public class CalibrationParser : IParser<Calibration>
    {
        Dictionary<string, string> numberMapping = new Dictionary<string, string>
        {
            {"zero", "zero0zero"},
            {"one", "one1one"},
            {"two", "two2two"},
            {"three", "three3three"},
            {"four", "four4four"},
            {"five", "five5five"},
            {"six", "six6six"},
            {"seven", "seven7seven"},
            {"eight", "eight8eight"},
            {"nine", "nine9nine"}
        };

        public IEnumerable<Calibration> Parse(string input)
        {
            var lines = input.Split("\r\n");
            foreach (var line in lines)
            {
                var numbers = Regex.Matches(line, "\\d").Select(x => int.Parse(x.Value)).ToArray();
                var spelledNumbers = Regex.Matches(Translate(line), "\\d").Select(x => int.Parse(x.Value)).ToArray();

                yield return new Calibration
                {
                    Value = GetValue(numbers),
                    SpelledValue = GetValue(spelledNumbers)
                };
            }
        }

        private string Translate(string line)
        {
            foreach (var map in numberMapping)
            {
                line = line.Replace(map.Key, map.Value);
            }
            return line;
        }

        private int GetValue(int[] numbers)
        {
            var value = (numbers[0] * 10) + numbers[numbers.Length - 1];
            return value;
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

        //        var test = int.Parse() * 10;
        //        var test2 = int.Parse(matches[matches.Count - 1].Value);

        //        value += test + test2;
        //    }

        //    return value;
        //}
    }

    public class Calibration : IParseModel
    {
        public int Value { get; set; }
        public int SpelledValue { get; set; }
    }
}