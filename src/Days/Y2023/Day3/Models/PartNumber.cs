namespace AdventOfCode.Y2023.Day3.Models
{
    public class PartNumber
    {
        public int Row { get; set; }

        public int Index { get; set; }

        public string StringValue { get; set; }

        public int Value { get { return int.Parse(StringValue); } }

        public List<int> Indices
        {
            get
            {
                var indices = new List<int>();
                for (int i = Index - 1; i <= Index + StringValue.Length; i++)
                {
                    if (i >= 0)
                        indices.Add(i);
                }
                return indices;
            }
        }
    }
}