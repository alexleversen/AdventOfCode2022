namespace AdventOfCode2022;

public class Day1Part2 : IPuzzleSolver
{
    private const int TopCount = 3;

    public string Run(string[] input)
    {
        var groupSum = 0;
        var maxValues = new int[TopCount];
        Array.Fill(maxValues, 0);
        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                for (var i = 0; i < TopCount; i++)
                {
                    if (groupSum < maxValues[i])
                    {
                        continue;
                    }

                    for (var j = TopCount - 2; j >= i; j--)
                    {
                        maxValues[j + 1] = maxValues[j];
                    }

                    maxValues[i] = groupSum;
                    break;
                }
                groupSum = 0;
                continue;
            }

            var parsedLineValue = int.Parse(line);
            groupSum += parsedLineValue;
        }

        return $"{maxValues.Sum()}";
    }
}