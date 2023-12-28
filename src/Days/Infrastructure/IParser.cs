namespace AdventOfCode.Infrastructure
{
    public interface IParser<out T> where T : IParseModel
    {
        IEnumerable<T> Parse(string input);
    }
}