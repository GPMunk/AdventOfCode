using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace AdventOfCode.Infrastructure
{
    public abstract class Solver
    {
        private string[] SplitNamespace => GetType().Namespace!.Split(".");
        public int Year => int.Parse(Regex.Match(SplitNamespace[1], "\\d+").Value);
        public int Day => int.Parse(Regex.Match(SplitNamespace[2], "\\d+").Value);

        public virtual bool WorkInProgress => false;

        public IEnumerable<SolveResult> Solve()
        {
            var input = GetInput();

            yield return SolvePartShell(1, SolvePartOne, input);
            yield return SolvePartShell(2, SolvePartTwo, input);
        }

        public IEnumerable<SolveTestResult> SolveTests()
        {
            var testInputs = GetTestInput();

            foreach (var testInput in testInputs)
            {
                SolveTestResult testResult;
                if (testInput.Part == 1)
                {
                    testResult = (SolveTestResult)SolvePartShell(1, SolvePartOne, testInput.SanitizedInput);
                }
                else
                {
                    testResult = (SolveTestResult)SolvePartShell(2, SolvePartTwo, testInput.SanitizedInput);
                }
                testResult.AnswerShouldBe = testInput.Answer;
                yield return testResult;
            }
        }

        public string GetInput()
        {
            var file = Path.Combine(SplitNamespace[1], SplitNamespace[2], "Input.txt");

            if (!File.Exists(file))
            {
                return "";
            }

            var input = File.ReadAllText(file);
            return input;
        }

        public IEnumerable<TestInput> GetTestInput()
        {
            var filename = Path.Combine(SplitNamespace[1], SplitNamespace[2], "TestInputXML.xml");

            if (!File.Exists(filename))
            {
                return Enumerable.Empty<TestInput>();
            }

            var serializer = new XmlSerializer(typeof(TestCases));

            TestCases testCases;
            using (Stream reader = new FileStream(filename, FileMode.Open))
            {
                testCases = (TestCases)serializer.Deserialize(reader);
            }

            if (!testCases.TestInputs.Any())
            {
                return Enumerable.Empty<TestInput>();
            }

            return testCases.TestInputs;
        }

        private SolveResult SolvePartShell(int part, Func<string, string> solvePart, string input)
        {
            var result = new SolveResult
            {
                Part = part
            };

            if (string.IsNullOrEmpty(input))
            {
                result.Answer = "No input";
                return result;
            }

            try
            {
                result.Answer = solvePart(input);
                return result;
            }
            catch (NotImplementedException nie)
            {
                result.Answer = $"{nie.GetType()}: {nie.Message}";
                return result;
            }
        }

        public abstract string SolvePartOne(string input);
        public abstract string SolvePartTwo(string input);
    }
}
