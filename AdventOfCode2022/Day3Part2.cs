namespace AdventOfCode2022;

public class Day3Part2 : IPuzzleSolver
{
    private const int LowercaseOffset = 96;
    private const int UppercaseOffset = 65 - 27;

    public string Run(string[] input)
    {
        var prioritySum = 0;
        for (var index = 0; index < input.Length; index += 3)
        {
            var group = input[index..(index + 3)];
            var duplicateItem = group[0].First(item => group[1].Contains(item) && group[2].Contains(item));
            switch (duplicateItem)
            {
                case >= 'a' and <= 'z':
                    prioritySum += duplicateItem - LowercaseOffset;
                    break;
                case >= 'A' and <= 'Z':
                    prioritySum += duplicateItem - UppercaseOffset;
                    break;
            }
        }

        return $"{prioritySum}";
    }
}