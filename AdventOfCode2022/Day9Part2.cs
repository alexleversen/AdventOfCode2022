using System.Text.RegularExpressions;
using AdventOfCode2022.Models.Day9;

namespace AdventOfCode2022;

public class Day9Part2 : IPuzzleSolver
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
            InnerSegments = new (int row, int col)[8],
            Tail = (0, 0)
        };
        for (var i = 0; i < currentState.InnerSegments.Length; i++)
        {
            currentState.InnerSegments[i] = (0, 0);
        }
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
                for(var segmentIndex = 0; segmentIndex < currentState.InnerSegments.Length; segmentIndex++)
                {
                    ComputeAndMoveSegment(currentState, currentState.InnerSegments[segmentIndex], segmentIndex);
                }
                var tailLocation = ComputeAndMoveSegment(currentState, currentState.Tail);
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

    private static (int row, int col) ComputeAndMoveSegment(RopeSimulation currentState, (int row, int col) segment, int segmentIndex = -1)
    {
        var (prevSegmentRow, prevSegmentCol) = segmentIndex switch
        {
            -1 => currentState.InnerSegments[^1],
            0 => currentState.Head,
            _ => currentState.InnerSegments[segmentIndex - 1]
        };
        if (Math.Abs(prevSegmentRow - segment.row) + Math.Abs(prevSegmentCol - segment.col) <= 1 || Math.Abs(prevSegmentRow - segment.row) == 1 && Math.Abs(prevSegmentCol - segment.col) == 1)
        {
            return (segment.row, segment.col);
        }

        var toRow = segment.row + Math.Sign(prevSegmentRow - segment.row);
        var toCol = segment.col + Math.Sign(prevSegmentCol - segment.col);

        if (segmentIndex == -1)
        {
            currentState.Tail = (toRow, toCol);
        }
        else
        {
            currentState.InnerSegments[segmentIndex] = (toRow, toCol);
        }

        return (toRow, toCol);
    }
}