﻿// See https://aka.ms/new-console-template for more information

namespace AdventOfCode2022;

public static class Program
{

    public static void Main(string[] args)
    {
        if (args.Length != 3)
        {
            Console.WriteLine("Usage: dotnet run <dayNumber> <partNumber> <inputFilePath>");
            return;
        }

        Run(args[0], args[1], args[2]);
    }

    public static void Run(string dayArg, string partArg, string inputFile)
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
                break;
        }

        if (solver is null)
        {
            return;
        }
        var input = File.ReadAllLines(inputFile);
        solver.Run(input);
    }
}