namespace AdventOfCode.Infrastructure
{
    public class TestInput
    {
        public int Part { get; set; }

        public string Input { get; set; }

        public string SanitizedInput => Input.Replace("\t", "").Replace("\n", "\r\n").Trim();

        public string Answer { get; set; }
    }
    public class TestCases
    {
        public TestInput[] TestInputs { get; set; }
    }
}
