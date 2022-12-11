using System.Text.RegularExpressions;

namespace AdventOfCode2022;

public class Day10Part1 : IPuzzleSolver
{
    private const string Noop = "noop";
    private const string AddX = "addx";

    private static readonly Dictionary<string, int> InstructionCycles = new(){
        [Noop] = 1,
        [AddX] = 2
    };

    private int _xRegisterValue = 1;

    public string Run(string[] input)
    {
        var cycle = 1;
        var instructionIndex = 0;
        var currentInstructionCycle = 1;
        var signalStrengthSum = 0;

        while (true)
        {
            var instructionGroups = new Regex("(\\w+)\\s?(-?\\d+)?").Match(input[instructionIndex]).Groups;
            var instruction = instructionGroups[1].Value;
            var value = instructionGroups[2].Value == "" ? null : instructionGroups[2].Value;
            if (cycle % 40 == 20)
            {
                signalStrengthSum += _xRegisterValue * cycle;
            }
            if (currentInstructionCycle == InstructionCycles[instruction])
            {
                try
                {
                    ExecuteInstruction(instruction, value == null ? null : int.Parse(value));
                }
                catch (Exception e)
                {
                    throw e;
                }

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

        return $"{signalStrengthSum}";
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
                _xRegisterValue += value.Value;
                return;
            default:
                throw new ArgumentException($"Invalid instruction {instruction}");
        }
    }
}