namespace AdventOfCode2022;

public class Day4Part2 : IPuzzleSolver
{
    public string Run(string[] input)
    {
        var overlapCount = input.ExtractRanges()
            .Count(rangePair => rangePair.HasOverlap());
        return $"{overlapCount}";
    }
}