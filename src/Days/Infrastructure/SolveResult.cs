namespace AdventOfCode.Infrastructure
{
    public class SolveResult
    {
        public int Part { get; set; }

        public string Answer { get; set; }
    }

    public class SolveTestResult : SolveResult
    {
        public string AnswerShouldBe { get; set; }
    }
}
