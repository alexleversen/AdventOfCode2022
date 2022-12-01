namespace AdventOfCode2022;

public class Day1Part1 : IPuzzleSolver
{
    public string Run(string[] input)
    {
        var groupSum = 0;
        var maxValue = 0;
        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                if (groupSum > maxValue)
                {
                    maxValue = groupSum;
                }
                groupSum = 0;
                continue;
            }

            var parsedLineValue = int.Parse(line);
            groupSum += parsedLineValue;
        }

        return $"{maxValue}";
    }
}