namespace AdventOfCode2022;

public class Day4Part1 : IPuzzleSolver
{
    public string Run(string[] input)
    {
        var containingCount = input.ExtractRanges()
            .Count(rangePair => rangePair[0].Contains(rangePair[1]) || rangePair[1].Contains(rangePair[0]));
        return $"{containingCount}";
    }
}