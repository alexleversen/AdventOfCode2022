using System.Text.RegularExpressions;

namespace AdventOfCode2022;

public class Day5Part2 : IPuzzleSolver
{
    public string Run(string[] input)
    {
        var crateIndexRegex = new Regex("^(\\s*\\d)+\\s*$");
        var stacks = new Stack<char>[9];
        for (var i = 0; i < 9; i++)
        {
            stacks[i] = new Stack<char>();
        }
        var cratesBottomIndex = input
            .Select((line, i) => (line, i))
            .First(tuple => crateIndexRegex.IsMatch(tuple.line))
            .i;
        for (var lineIndex = cratesBottomIndex - 1; lineIndex >= 0; lineIndex--)
        {
            var currentLine = input[lineIndex];
            const int columnWidth = 4;
            for (var charIndex = 1; charIndex < currentLine.Length; charIndex += columnWidth)
            {
                var currentChar = currentLine[charIndex];
                if (currentChar == ' ') continue;
                var stackIndex = (charIndex - 1) / columnWidth;
                stacks[stackIndex].Push(currentChar);
            }
        }

        var instructionRegex = new Regex("^move (\\d+) from (\\d) to (\\d)");
        for (var instructionIndex = cratesBottomIndex + 2; instructionIndex < input.Length; instructionIndex++)
        {
            var groups = instructionRegex.Match(input[instructionIndex]).Groups;
            var crateCount = int.Parse(groups[1].Value);
            var fromStack = int.Parse(groups[2].Value) - 1;
            var toStack = int.Parse(groups[3].Value) - 1;

            var tempStack = new Stack<char>();
            for (var crateIndex = 0; crateIndex < crateCount; crateIndex++)
            {
                tempStack.Push(stacks[fromStack].Pop());
            }

            for (var crateIndex = 0; crateIndex < crateCount; crateIndex++)
            {
                stacks[toStack].Push(tempStack.Pop());
            }
        }

        return stacks.Aggregate("", (current, stack) => current + stack.Pop());
    }
}