using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2022;

public class Day10Part2 : IPuzzleSolver
{
    private const string Noop = "noop";
    private const string AddX = "addx";
    private const int ScreenWidth = 40;
    private const int ScreenHeight = 6;

    private static readonly Dictionary<string, int> InstructionCycles = new(){
        [Noop] = 1,
        [AddX] = 2
    };

    private int _spritePosition = 1;

    public string Run(string[] input)
    {
        var cycle = 0;
        var instructionIndex = 0;
        var currentInstructionCycle = 1;
        var screenChars = new char[ScreenHeight][];
        for(var i = 0; i < ScreenHeight; i++)
        {
            var newRow = new char[ScreenWidth];
            Array.Fill(newRow, '.');
            screenChars[i] = newRow;
        }

        while (true)
        {
            var instructionGroups = new Regex("(\\w+)\\s?(-?\\d+)?").Match(input[instructionIndex]).Groups;
            var instruction = instructionGroups[1].Value;
            var value = instructionGroups[2].Value == "" ? null : instructionGroups[2].Value;
            if (cycle % ScreenWidth >= _spritePosition - 1 && cycle % ScreenWidth <= _spritePosition + 1)
            {
                screenChars[cycle / ScreenWidth][cycle % ScreenWidth] = '#';
            }
            if (currentInstructionCycle == InstructionCycles[instruction])
            {
                ExecuteInstruction(instruction, value == null ? null : int.Parse(value));

                currentInstructionCycle = 1;
                instructionIndex++;
            }
            else
            {
                currentInstructionCycle++;
            }

            if (instructionIndex >= input.Length)
            {
                break;
            }

            cycle++;
        }

        return ToString(screenChars);
    }

    private static string ToString(IEnumerable<char[]> charGrid)
    {
        var sb = new StringBuilder();

        foreach (var row in charGrid)
        {
            foreach (var c in row)
            {
                sb.Append(c);
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }

    private void ExecuteInstruction(string instruction, int? value)
    {
        switch (instruction)
        {
            case Noop:
                return;
            case AddX:
                if (!value.HasValue)
                {
                    throw new ArgumentException("Missing argument to addx instruction");
                }
                _spritePosition += value.Value;
                return;
            default:
                throw new ArgumentException($"Invalid instruction {instruction}");
        }
    }
}