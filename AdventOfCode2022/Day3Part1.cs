namespace AdventOfCode2022;

public class Day3Part1 : IPuzzleSolver
{
    private const int LowercaseOffset = 96;
    private const int UppercaseOffset = 65 - 27;
    public string Run(string[] input)
    {
        var prioritySum = 0;
        foreach (var rucksack in input)
        {
            var sectionLength = rucksack.Length / 2;
            var compartments = new[]
            {
                rucksack[..sectionLength],
                rucksack[sectionLength..]
            };
            var duplicateItem = compartments[0].First(item => compartments[1].Contains(item));
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