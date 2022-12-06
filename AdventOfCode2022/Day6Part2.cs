namespace AdventOfCode2022;

public class Day6Part2 : IPuzzleSolver
{
    private const int MarkerLength = 14;
    public string Run(string[] input)
    {
        var inputLine = input[0];

        for(var charIndex = MarkerLength - 1; charIndex < inputLine.Length; charIndex++)
        {
            var currentChunk = inputLine.Substring(charIndex - (MarkerLength - 1), MarkerLength);
            var hashSet = new HashSet<char> { currentChunk[0] };
            var unique = currentChunk[1..].All(c => hashSet.Add(c));

            if (unique)
            {
                return $"{charIndex + 1}";
            }
        }

        return "";
    }
}