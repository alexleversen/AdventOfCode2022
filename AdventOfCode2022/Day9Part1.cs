using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2022;

public class Day9Part1 : IPuzzleSolver
{
    private const int SimulationWidth = 1000;
    private const int SimulationHeight = 1000;
    private const string Right = "R";
    private const string Left = "L";
    private const string Up = "U";
    private const string Down = "D";

    public string Run(string[] input)
    {
        var ropeSimulation = new char[SimulationHeight][];
        var visitedLocations = new List<(int row, int col)>();
        for (var i = 0; i < SimulationHeight; i++)
        {
            ropeSimulation[i] = new char[SimulationWidth];
            Array.Fill(ropeSimulation[i], '.');
        }

        ropeSimulation[SimulationHeight / 2][SimulationWidth / 2] = 'H';
        // Console.WriteLine("Initial state");
        // Console.WriteLine(ToString(ropeSimulation));

        foreach (var line in input)
        {
            Console.WriteLine($"Command: {line}");
            var groups = new Regex("(\\w) (\\d)").Match(line).Groups;
            var direction = groups[1].Value;
            var count = int.Parse(groups[2].Value);
            for (var i = 0; i < count; i++)
            {
                // Console.WriteLine("Moving head...");
                MoveHead(ropeSimulation, direction);
                // Console.WriteLine(ToString(ropeSimulation));
                // Console.WriteLine("Moving tail...");
                visitedLocations.Add(ComputeAndMoveTail(ropeSimulation));
                // Console.WriteLine(ToString(ropeSimulation));
            }
        }

        var locationCount = visitedLocations.Distinct().Count();

        return $"{locationCount}";
    }

    private static string ToString(IEnumerable<char[]> state)
    {
        var sb = new StringBuilder();
        foreach (var line in state)
        {
            foreach(var currentChar in line)
            {
                sb.Append(currentChar);
                sb.Append('\t');
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }

    private static (int row, int col) FindEnd(IReadOnlyList<char[]> currentState, char endChar)
    {
        var (row, col) = (-1, -1);
        var endFound = false;
        for (var i = 0; i < currentState.Count; i++)
        {
            for (var j = 0; j < currentState[i].Length; j++)
            {
                if (currentState[i][j] != endChar) continue;
                (row, col) = (i, j);
                endFound = true;
                break;
            }

            if (endFound)
            {
                break;
            }
        }

        if (endChar == 'T' && !endFound)
        {
            (row, col) = FindEnd(currentState, 'H');
        }

        return (row, col);
    }

    private static void MoveHead(IReadOnlyList<char[]> currentState, string direction)
    {
        var (row, col) = FindEnd(currentState, 'H');
        var (tailRow, tailCol) = FindEnd(currentState, 'T');
        var replacementChar = '.';
        if (row == tailRow && col == tailCol)
        {
            replacementChar = 'T';
        }
        switch (direction)
        {
            case Left:
                currentState[row][col - 1] = 'H';
                break;
            case Right:
                currentState[row][col + 1] = 'H';
                break;
            case Up:
                currentState[row - 1][col] = 'H';
                break;
            case Down:
                currentState[row + 1][col] = 'H';
                break;
            default:
                throw new InvalidOperationException("Movement not allowed");
        }

        currentState[row][col] = replacementChar;
    }

    private static void MoveTail(IReadOnlyList<char[]> currentState, int fromRow, int fromCol, int toRow, int toCol)
    {
        currentState[fromRow][fromCol] = '.';
        currentState[toRow][toCol] = 'T';
    }

    private static (int row, int col) ComputeAndMoveTail(IReadOnlyList<char[]> currentState)
    {
        var (headRow, headCol) = FindEnd(currentState, 'H');
        var (tailRow, tailCol) = FindEnd(currentState, 'T');
        if (Math.Abs(headRow - tailRow) + Math.Abs(headCol - tailCol) <= 1 || Math.Abs(headRow - tailRow) == 1 && Math.Abs(headCol - tailCol) == 1)
        {
            return (tailRow, tailCol);
        }

        var toRow = tailRow + Math.Sign(headRow - tailRow);
        var toCol = tailCol + Math.Sign(headCol - tailCol);
        MoveTail(currentState, tailRow, tailCol, toRow, toCol);

        return (toRow, toCol);
    }
}