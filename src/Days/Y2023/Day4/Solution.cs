﻿using AdventOfCode.Infrastructure;

namespace AdventOfCode.Y2023.Day4
{
    public class Solution : Solver
    {
        public override SolveResult SolvePartOne(string input)
        {
            var cards = CardParser.Parse(input);
            var totalPoints = cards.Sum(x => x.Points);

            return new SolveResult
            {
                Part = 1,
                Answer = totalPoints.ToString()
            };
        }

        public override SolveResult SolvePartTwo(string input)
        {
            var cards = CardParser.Parse(input).ToArray();

            var counts = cards.Select(_ => 1).ToArray();

            for (var i = 0; i < cards.Length; i++)
            {
                var (card, count) = (cards[i], counts[i]);
                for (var j = 0; j < card.MatchingNumbers; j++)
                {
                    counts[i + j + 1] += count;
                }
            }

            return new SolveResult
            {
                Part = 2,
                Answer = counts.Sum().ToString()
            };
        }
    }
}
