using System.Text.RegularExpressions;
using AdventOfCode2022.Models.Day9;

namespace AdventOfCode2022;

public class Day9Part1 : IPuzzleSolver
{
    private const string Right = "R";
    private const string Left = "L";
    private const string Up = "U";
    private const string Down = "D";

    public string Run(string[] input)
    {
        var currentState = new RopeSimulation
        {
            Head = (0, 0),
            Tail = (0, 0)
        };
        var visitedLocations = new List<(int row, int col)>
        {
            (0, 0)
        };


        foreach (var line in input)
        {
            var groups = new Regex("(\\w) (\\d+)").Match(line).Groups;
            var direction = groups[1].Value;
            var count = int.Parse(groups[2].Value);
            for (var i = 0; i < count; i++)
            {
                MoveHead(currentState, direction);
                var tailLocation = ComputeAndMoveTail(currentState);
                if (!visitedLocations.Contains(tailLocation))
                {
                    visitedLocations.Add(tailLocation);
                }
            }
        }

        var locationCount = visitedLocations.Count;

        return $"{locationCount}";
    }

    private static void MoveHead(RopeSimulation currentState, string direction)
    {
        var (row, col) = currentState.Head;
        switch (direction)
        {
            case Left:
                currentState.Head = (row, col - 1);
                break;
            case Right:
                currentState.Head = (row, col + 1);
                break;
            case Up:
                currentState.Head = (row - 1, col);
                break;
            case Down:
                currentState.Head = (row + 1, col);
                break;
            default:
                throw new InvalidOperationException("Movement not allowed");
        }
    }

    private static (int row, int col) ComputeAndMoveTail(RopeSimulation currentState)
    {
        var (headRow, headCol) = currentState.Head;
        var (tailRow, tailCol) = currentState.Tail;
        if (Math.Abs(headRow - tailRow) + Math.Abs(headCol - tailCol) <= 1 || Math.Abs(headRow - tailRow) == 1 && Math.Abs(headCol - tailCol) == 1)
        {
            return (tailRow, tailCol);
        }

        var toRow = tailRow + Math.Sign(headRow - tailRow);
        var toCol = tailCol + Math.Sign(headCol - tailCol);
        currentState.Tail = (toRow, toCol);

        return (toRow, toCol);
    }
}