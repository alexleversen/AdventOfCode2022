namespace AdventOfCode2022.Models.Day11;

public class Monkey
{
    public Monkey()
    {
        Items = new List<int>();
    }

    public List<int> Items { get; }
    public Func<int, int>? Operation { get; set; }
    public int Divisor { get; set; }
    public int? TrueIndex { get; set; }
    public int? FalseIndex { get; set; }
    public int InspectionCount { get; set; }
}