// See https://aka.ms/new-console-template for more information

namespace AdventOfCode2022;

public static class Program
{
    public static void Main(string[] args)
    {
        if (args.Length != 2 && args.Length != 3)
        {
            Console.WriteLine("Usage: dotnet run <dayNumber> <partNumber> (<fileName>, optional)");
            return;
        }

        Console.WriteLine(Run(args));
    }

    private static string Run(params string[] args)//string dayArg, string partArg)
    {
        var dayArg = args[0];
        var partArg = args[1];
        IPuzzleSolver? solver = null;
        var day = int.Parse(dayArg);
        var part = int.Parse(partArg);
        var fileName = args.Length == 3 ? $"{args[2]}" : $"inputs/{day:D2}.txt";
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
            case 4:
                if (part == 1)
                {
                    solver = new Day4Part1();
                }
                else
                {
                    solver = new Day4Part2();
                }
                break;
            case 5:
                if (part == 1)
                {
                    solver = new Day5Part1();
                }
                else
                {
                    solver = new Day5Part2();
                }
                break;
            case 6:
                if (part == 1)
                {
                    solver = new Day6Part1();
                }
                else
                {
                    solver = new Day6Part2();
                }
                break;
            case 7:
                if (part == 1)
                {
                    solver = new Day7Part1();
                }
                else
                {
                    solver = new Day7Part2();
                }
                break;
            case 8:
                if (part == 1)
                {
                    solver = new Day8Part1();
                }
                else
                {
                    solver = new Day8Part2();
                }
                break;
            case 9:
                if (part == 1)
                {
                    solver = new Day9Part1();
                }
                break;
        }

        if (solver is null)
        {
            return "";
        }

        var input = File.ReadAllLines(fileName);
        return solver.Run(input);
    }
}