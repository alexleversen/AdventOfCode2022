namespace AdventOfCode2022;

public class Day8Part2 : IPuzzleSolver
{
    public string Run(string[] input)
    {
        var maxVisibilityScore = 0;
        for (var lineIndex = 0; lineIndex < input.Length; lineIndex++)
        {
            var line = input[lineIndex];
            for (var charIndex = 0; charIndex < line.Length; charIndex++)
            {
                var visibilityScore = GetVisibilityScore(input, lineIndex, charIndex);
                if (visibilityScore > maxVisibilityScore)
                {
                    maxVisibilityScore = visibilityScore;
                }
            }
        }

        return $"{maxVisibilityScore}";
    }

    private static int GetVisibilityScore(string[] input, int x, int y)
    {
        var currentValue = int.Parse(input[x][y].ToString());
        if (x == 0 || x == input.Length - 1 || y == 0 || y == input.Length - 1)
        {
            return 0;
        }

        var leftDirectionalValues = input[..x].Select(l => int.Parse(l[y].ToString())).Reverse().ToArray();
        var leftVisibility = CalculateDirectionalVisibility(currentValue, leftDirectionalValues);
        var rightDirectionalValues = input[(x + 1)..].Select(l => int.Parse(l[y].ToString())).ToArray();
        var rightVisibility = CalculateDirectionalVisibility(currentValue, rightDirectionalValues);
        var upDirectionalValues = input[x][..y].Select(l => int.Parse(l.ToString())).Reverse().ToArray();
        var upVisibility = CalculateDirectionalVisibility(currentValue, upDirectionalValues);
        var downDirectionalValues = input[x][(y + 1)..].Select(l => int.Parse(l.ToString())).ToArray();
        var downVisibility = CalculateDirectionalVisibility(currentValue, downDirectionalValues);

        return leftVisibility * rightVisibility * upVisibility * downVisibility;
    }

    private static int CalculateDirectionalVisibility(int currentValue, IReadOnlyCollection<int> directionalValues)
    {
        return directionalValues.Any(v => v >= currentValue)
            ? directionalValues
                .Select((v, i) => (v, i))
                .First(tuple => tuple.v >= currentValue).i + 1
            : directionalValues.Count;
    }
}