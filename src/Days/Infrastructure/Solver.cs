namespace AdventOfCode.Infrastructure
{
    public abstract class Solver
    {
        private string[] SplitNamespace => GetType().Namespace!.Split(".");
        public int Year => int.Parse(SplitNamespace[1][1..]);
        public int Day => int.Parse(SplitNamespace[2].Last().ToString());

        public string GetInput()
        {
            var input = File.ReadAllText(Path.Combine(SplitNamespace[1], SplitNamespace[2], "Input.txt"));
            return input;
        }

        public IEnumerable<SolveResult> Solve()
        {
            var input = GetInput();
            yield return SolvePartOne(input);
            yield return SolvePartTwo(input);
        }

        public abstract SolveResult SolvePartOne(string input);
        public abstract SolveResult SolvePartTwo(string input);
    }
}
