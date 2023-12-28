using AdventOfCode.Infrastructure;

namespace AdventOfCode.Y2023.Day2.Models
{
    public class Game : IParseModel
    {
        public int Id { get; set; }

        public List<Reveal> Reveals { get; set; }

        public bool IsPossible
        {
            get
            {
                return !Reveals.Any(x => !x.IsPossible);
            }
        }

        public int PowerSum
        {
            get
            {
                return Reveals.Max(x => x.MaxBlueCubes) * Reveals.Max(x => x.MaxRedCubes) * Reveals.Max(x => x.MaxGreenCubes);
            }
        }
    }
}