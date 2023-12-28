using AdventOfCode.Infrastructure;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2023.Day7
{
    public class Solution : Solver
    {
        Dictionary<string, int> CardMap = new Dictionary<string, int>
        {
            { "1", 1 },
            { "2", 2 },
            { "3", 3 },
            { "4", 4 },
            { "5", 5 },
            { "6", 6 },
            { "7", 7 },
            { "8", 8 },
            { "9", 9 },
            { "T", 10 },
            { "J", 11 },
            { "Q", 12 },
            { "K", 13 },
            { "A", 14 }
        };

        public override string SolvePartOne(string input)
        {
            var hands = Parse(input);
            var totalWinnings = CalculateWinnings(hands);

            return totalWinnings.ToString();
        }

        public override string SolvePartTwo(string input)
        {
            var hands = Parse(input).ToList();
            foreach ( var hand in hands)
            {
                var newCards = hand.Cards.ToList();
                if (!newCards.Exists(x => x.Name == "J"))
                {
                    continue;
                }

                if (newCards.Count(x => x.Name == "J") == 5)
                {
                    foreach (var card in newCards)
                    {
                        card.Update("J", 0);
                    }
                    hand.Cards = newCards;
                    continue;
                }

                var cardGroups = newCards.Where(x => x.Name != "J").OrderByDescending(x => x.Value).GroupBy(x => x.Value).OrderByDescending(x => x.Count());//.ThenBy(x => x.Key);
                foreach (var card in newCards)
                {
                    if (card.Name == "J")
                    {
                        card.Update(cardGroups.First().First().Name, 0);
                    }
                }

                hand.Cards = newCards;
            }

            var totalWinnings = CalculateWinnings(hands);

            return totalWinnings.ToString();
        }

        private IEnumerable<Hand> Parse(string input)
        {
            var lines = input.Split("\r\n");

            var hands = lines.Select(x => new Hand
            {
                Cards = Regex.Matches(x.Split(" ")[0], "\\S").Select(x => new Card { Name = x.Value, Value = CardMap[x.Value] }),
                Bid = long.Parse(x.Split(" ")[1])
            });
            return hands;
        }

        private long CalculateWinnings(IEnumerable<Hand> hands)
        {
            var orderedHands = hands.OrderBy(x => x.HandType)
                    .ThenBy(x => x.Cards.First().Value)
                    .ThenBy(x => x.Cards.Skip(1).Take(1).First().Value)
                    .ThenBy(x => x.Cards.Skip(2).Take(1).First().Value)
                    .ThenBy(x => x.Cards.Skip(3).Take(1).First().Value)
                    .ThenBy(x => x.Cards.Last().Value);

            var totalWinnings = 0L;
            var index = 1;
            foreach (var hand in orderedHands)
            {
                totalWinnings += hand.Bid * index;
                index++;
            }

            return totalWinnings;
        }
    }

    public class Hand : IParseModel
    {
        public IEnumerable<Card> Cards { get; set; }
        public long Bid { get; set; }

        public string StringRep => string.Join(',', Cards.Select(x => $"{x.Name}{x.Value}"));

        public override string ToString() => StringRep;

        public HandType HandType
        {
            get
            {
                var test = Cards.Distinct().GroupBy(x => x.Name).OrderByDescending(x => x.Count());
                switch (test.First().Count())
                {
                    case 5:
                        return HandType.FiveOfAKind;
                    case 4:
                        return HandType.FourOfAKind;
                    case 3:
                        if (test.Count() == 2)
                            return HandType.FullHouse;
                        else
                            return HandType.ThreeOfAKind;
                    case 2:
                        if (test.Skip(1).Count() == 2)
                            return HandType.TwoPair;
                        else
                            return HandType.OnePair;
                    default:
                        return HandType.HighCard;
                }
            }
        }
    }

    public enum HandType
    {
        HighCard,
        OnePair,
        TwoPair,
        ThreeOfAKind,
        FullHouse,
        FourOfAKind,
        FiveOfAKind
    }

    public class Card
    {
        public string Name { get; set; }

        public int Value { get; set; }

        public void Update(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public override string ToString() => $"{Name}{Value}";
    }
}
