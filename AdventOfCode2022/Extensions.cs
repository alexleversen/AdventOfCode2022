using System.Text;

namespace AdventOfCode2022;

public static class Extensions
{
	public static IEnumerable<List<Range>> ExtractRanges(this IEnumerable<string> input)
	{
		return input
			.Select(line =>
				line.Split(",")
					.Select(range =>
						{
							var indices = range
								.Split("-")
								.Select(int.Parse)
								.ToList();
							if (indices is not { Count: 2 })
							{
								throw new InvalidDataException($"Invalid input: {line}");
							}

							return new Range(indices[0], indices[1]);
						}
					)
					.ToList()
			).ToList();
	}

	public static bool Contains(this Range current, Range other) => current.Start.Value <= other.Start.Value && current.End.Value >= other.End.Value;

	public static bool HasOverlap(this IList<Range> ranges) => ranges[0].Overlaps(ranges[1]);

	public static string ToString(IEnumerable<char[]> charGrid)
	{
		var sb = new StringBuilder();

		foreach (var row in charGrid)
		{
			foreach (var c in row)
			{
				sb.Append(c);
			}

			sb.AppendLine();
		}

		return sb.ToString();
	}

	private static bool Overlaps(this Range current, Range other) =>
		(current.Start.Value >= other.Start.Value && current.Start.Value <= other.End.Value)
		|| (current.End.Value >= other.Start.Value && current.End.Value <= other.End.Value)
		|| current.Contains(other);
}