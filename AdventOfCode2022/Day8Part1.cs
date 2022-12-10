namespace AdventOfCode2022;

public class Day8Part1 : IPuzzleSolver
{
    public string Run(string[] input)
    {
        var visibleCoords = new List<(int x, int y)>();
        for (var lineIndex = 0; lineIndex < input.Length; lineIndex++)
        {
            var line = input[lineIndex];
            for (var charIndex = 0; charIndex < line.Length; charIndex++)
            {
                if (IsVisible(input, lineIndex, charIndex))
                {
                    visibleCoords.Add((lineIndex, charIndex));
                }
            }
        }

        var visibleCount = visibleCoords.DistinctBy(tuple => $"{tuple.x},{tuple.y}").Count();

        return $"{visibleCount}";
    }

    private static bool IsVisible(string[] input, int x, int y)
    {
        var currentValue = int.Parse(input[x][y].ToString());
        var retVal = x == 0
                     || x == input.Length - 1
                     || y == 0
                     || y == input[x].Length - 1
                     || input[x][(y + 1)..].All(c => int.Parse(c.ToString()) < currentValue)
                     || input[x][..y].All(c => int.Parse(c.ToString()) < currentValue)
                     || input[(x + 1)..].Select(i => i[y]).All(c => int.Parse(c.ToString()) < currentValue)
                     || input[..x].Select(i => i[y]).All(c => int.Parse(c.ToString()) < currentValue);
        return retVal;
    }
}