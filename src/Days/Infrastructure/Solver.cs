using System.Text.RegularExpressions;

namespace AdventOfCode.Infrastructure
{
    public abstract class Solver
    {
        private string[] SplitNamespace => GetType().Namespace!.Split(".");
        public int Year => int.Parse(Regex.Match(SplitNamespace[1], "\\d+").Value);
        public int Day => int.Parse(Regex.Match(SplitNamespace[2], "\\d+").Value);

        public virtual bool WorkInProgress => false;

        public string GetInput()
        {
            var filename = "Input.txt";
            if (WorkInProgress)
            {
                filename = $"Test{filename}";
            }

            var file = Path.Combine(SplitNamespace[1], SplitNamespace[2], filename);

            if (!File.Exists(file))
            {
                return "";
            }

            var input = File.ReadAllText(file);
            return input;
        }

        public IEnumerable<SolveResult> Solve()
        {
            var input = GetInput();

            yield return SolvePartShell(1, SolvePartOne, input);
            yield return SolvePartShell(2, SolvePartTwo, input);
        }

        private SolveResult SolvePartShell(int part, Func<string, string> solvePart, string input)
        {
            var result = new SolveResult
            {
                Part = part
            };

            if (string.IsNullOrEmpty(input))
            {
                result.Answer = "No input";
                return result;
            }

            try
            {
                result.Answer = solvePart(input);
                return result;
            }
            catch (NotImplementedException nie)
            {
                result.Answer = $"{nie.GetType()}: {nie.Message}";
                return result;
            }
        }

        public abstract string SolvePartOne(string input);
        public abstract string SolvePartTwo(string input);
    }
}
