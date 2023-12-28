using AdventOfCode.Infrastructure;
using AdventOfCode.Y2023.Day2.Models;

namespace AdventOfCode.Y2023.Day2
{
    public class Solution : Solver
    {
        public IParser<Game> Parser = new GameParser();
        public IEnumerable<Game> Games { get; set; }

        public override SolveResult SolvePartOne(string input)
        {
            var games = GetGames(input);
            var possibleGames = games.Where(x => x.IsPossible);
            var sum = possibleGames.Sum(x => x.Id);

            return new SolveResult
            {
                Part = 1,
                Answer = sum.ToString()
            };
        }

        public override SolveResult SolvePartTwo(string input)
        {
            var games = GetGames(input);
            return new SolveResult
            {
                Part = 2,
                Answer = games.Sum(x => x.PowerSum).ToString()
            };
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
