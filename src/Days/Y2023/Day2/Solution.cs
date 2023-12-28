using AdventOfCode.Infrastructure;
using AdventOfCode.Y2023.Day2.Models;

namespace AdventOfCode.Y2023.Day2
{
    public class Solution : Solver
    {
        private readonly IParser<Game> Parser = new GameParser();
        private IEnumerable<Game> Games { get; set; }

        public override string SolvePartOne(string input)
        {
            var games = GetGames(input);
            var possibleGames = games.Where(x => x.IsPossible);
            var sum = possibleGames.Sum(x => x.Id);

            return sum.ToString();
        }

        public override string SolvePartTwo(string input)
        {
            var games = GetGames(input);
            return games.Sum(x => x.PowerSum).ToString();
        }

        private IEnumerable<Game> GetGames(string input)
        {
            if (Games == null)
            {
                Games = Parser.Parse(input);
            }
            return Games;
        }
    }
}
