// See https://aka.ms/new-console-template for more information

namespace AdventOfCode2022;

public static class Program
{

    public static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: dotnet run <dayNumber> <partNumber>");
            return;
        }

        Console.WriteLine(Run(args[0], args[1]));
    }

    private static string Run(string dayArg, string partArg)
    {
        IPuzzleSolver? solver = null;
        var day = int.Parse(dayArg);
        var part = int.Parse(partArg);

        switch (day)
        {
            case 1:
                if (part == 1)
                {
                    solver = new Day1Part1();
                }
                else
                {
                    solver = new Day1Part2();
                }
                break;
            case 2:
                if (part == 1)
                {
                    solver = new Day2Part1();
                }
                else
                {
                    solver = new Day2Part2();
                }
                break;
            case 3:
                if (part == 1)
                {
                    solver = new Day3Part1();
                }
                else
                {
                    solver = new Day3Part2();
                }
                break;
        }

        if (solver is null)
        {
            return "";
        }

        var input = File.ReadAllLines($"inputs/{day:D2}.txt");
        return solver.Run(input);
    }
}