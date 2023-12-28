namespace AdventOfCode.Infrastructure
{
    public abstract class Solver
    {
        private string[] SplitNamespace => GetType().Namespace!.Split(".");
        public int Year => int.Parse(SplitNamespace[1][1..]);
        public int Day => int.Parse(SplitNamespace[2].Last().ToString());

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

            if (string.IsNullOrEmpty(input))
            {
                yield return new SolveResult
                {
                    Part = 0,
                    Answer = "No input"
                };
                yield break;
            }

            yield return SolvePartOne(input);
            yield return SolvePartTwo(input);
        }

        public abstract SolveResult SolvePartOne(string input);
        public abstract SolveResult SolvePartTwo(string input);
    }
}
