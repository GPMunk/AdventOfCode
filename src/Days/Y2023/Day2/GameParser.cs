using AdventOfCode.Infrastructure;
using AdventOfCode.Y2023.Day2.Models;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2023.Day2
{
    public class GameParser : IParser<Game>
    {
        public IEnumerable<Game> Parse(string input)
        {
            return input.Split("\r\n").Select(ParseLine);
        }

        public Game ParseLine(string line)
        {
            var splitLine = line.Split(':');
            var gameStr = splitLine[0];
            var revealsString = splitLine[1];

            var id = int.Parse(Regex.Match(gameStr, "\\d+").Value);

            var revealsStr = revealsString.Split(';');
            var reveals = new List<Reveal>();
            foreach (var revealStr in revealsStr)
            {
                var cubesStr = revealStr.Split(",");

                var reveal = new Reveal();
                foreach (var cubeStr in cubesStr)
                {
                    var numberOfCubes = int.Parse(Regex.Match(cubeStr, "\\d+").Value);
                    var colourStr = Regex.Match(cubeStr, "[a-zA-Z]+").Value;
                    var colour = GetColour(colourStr);
                    reveal.Cubes.TryAdd(colour, numberOfCubes);
                }
                reveals.Add(reveal);
            }

            return new Game
            {
                Id = id,
                Reveals = reveals
            };
        }

        private Colour GetColour(string colourStr)
        {
            switch (colourStr)
            {
                case "green":
                    return Colour.Green;
                case "red":
                    return Colour.Red;
                case "blue":
                    return Colour.Blue;
                default:
                    throw new NotSupportedException($"{colourStr} is not supported");
            }
        }
    }
}