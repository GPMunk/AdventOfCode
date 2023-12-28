using System.Text.RegularExpressions;

namespace AdventOfCode.Y2023.Day4
{
    public static class CardParser
    {
        public static List<Card> Parse(string input)
        {
            var cards = new List<Card>();
            foreach (var line in input.Split("\r\n"))
            {
                var splitCard = line.Split(':');
                var id = int.Parse(Regex.Match(splitCard[0], "\\d+").Value);

                var splitNumbers = splitCard[1].Split('|');

                var splitWinningNumbers = splitNumbers[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var winningNumbers = splitWinningNumbers.Select(int.Parse).ToList();

                var splitOwnNumbers = splitNumbers[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var ownNumbers = splitOwnNumbers.Select(int.Parse).ToList();

                cards.Add(new Card
                {
                    Id = id,
                    WinningNumbers = winningNumbers,
                    OwnNumbers = ownNumbers
                });
            }

            return cards;
        }
    }

    public class Card
    {
        public int Id { get; set; }

        public List<int> WinningNumbers { get; set; } = new List<int>();

        public List<int> OwnNumbers { get; set; } = new List<int>();

        public int MatchingNumbers => WinningNumbers.Intersect(OwnNumbers).Count();

        public double Points => Math.Floor(Math.Pow(2, MatchingNumbers - 1));
    }
}