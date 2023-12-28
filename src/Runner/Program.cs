// See https://aka.ms/new-console-template for more information

using AdventOfCode.Infrastructure;
using System.Reflection;

var solversByYears = Assembly.GetAssembly(typeof(Solver))!.GetTypes()
    .Where(t => t.GetTypeInfo().IsClass && typeof(Solver).IsAssignableFrom(t) && !t.IsAbstract)
    .Select(t => (Solver)Activator.CreateInstance(t)!)
    .GroupBy(s => s.Year)
    .ToArray();

foreach (var solversByYear in solversByYears)
{
    Console.WriteLine($"Solutions for {solversByYear.Key}");
    RunSolversByYear(solversByYear.OrderBy(s => s.Day));
    Console.WriteLine();
}

void RunSolversByYear(IEnumerable<Solver> solversByYear)
{
    foreach (var solver in solversByYear)
    {
        Console.WriteLine($"Day {solver.Day}");
        RunSolver(solver);
        Console.WriteLine();
    }
}

void RunSolver(Solver solver)
{
    var input = solver.GetInput();
    var results = solver.Solve();

    foreach (var result in results)
    {
        Console.WriteLine($"Part {result.Part}: {result.Answer}");
    }
}
