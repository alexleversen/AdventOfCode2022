namespace AdventOfCode2022.Models.Day9;

public class RopeSimulation
{
    public (int row, int col) Head { get; set; }
    public (int row, int col)[] InnerSegments { get; set; } = Array.Empty<(int row, int col)>();
    public (int row, int col) Tail { get; set; }
}