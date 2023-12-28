namespace AdventOfCode.Y2023.Day2.Models
{
    public class Reveal
    {
        public Dictionary<Colour, int> Cubes { get; set; } = new Dictionary<Colour, int>();

        public bool IsPossible
        {
            get
            {
                return MaxRedCubes <= 12 && MaxGreenCubes <= 13 && MaxBlueCubes <= 14;
            }
        }

        public int MaxRedCubes { get { return Cubes.ContainsKey(Colour.Red) ? Cubes.Where(x => x.Key == Colour.Red).Max(x => x.Value) : 0; } }
        public int MaxBlueCubes { get { return Cubes.ContainsKey(Colour.Blue) ? Cubes.Where(x => x.Key == Colour.Blue).Max(x => x.Value) : 0; } }
        public int MaxGreenCubes { get { return Cubes.ContainsKey(Colour.Green) ? Cubes.Where(x => x.Key == Colour.Green).Max(x => x.Value) : 0; } }
    }
}