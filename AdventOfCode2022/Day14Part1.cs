namespace AdventOfCode2022;

public class Day14Part1 : IPuzzleSolver
{
	private int _xOffset;
	public string Run(string[] input)
	{
		var allPoints = input
			.Select(line =>
				line.Split(" -> ")
					.Select(pointString =>
						pointString.Split(",")
							.Select(int.Parse)
							.ToList()
					).ToList()
			).ToList();
		var xMax = allPoints.Select(lineSeries => lineSeries.Max(point => point[0])).Max();
		_xOffset = allPoints.Select(lineSeries => lineSeries.Min(point => point[0])).Min();
		var yMax = allPoints.Select(lineSeries => lineSeries.Max(point => point[1])).Max();

		var grid = new char[yMax + 1][];
		for (var i = 0; i <= yMax; i++)
		{
			var row = new char[xMax - _xOffset + 1];
			Array.Fill(row, '.');
			grid[i] = row;
		}

		grid[0][500 - _xOffset] = '+';

		foreach (var line in allPoints)
		{
			for (var pointIndex = 0; pointIndex < line.Count - 1; pointIndex++)
			{
				if (line[pointIndex][0] == line[pointIndex + 1][0])
				{
					var point1 = line[pointIndex][1];
					var point2 = line[pointIndex + 1][1];
					var startPoint = point1 < point2 ? point1 : point2;
					var endPoint = point1 < point2 ? point2 : point1;
					for (var i = startPoint; i <= endPoint; i++)
					{
						grid[i][line[pointIndex][0] - _xOffset] = '#';
					}
				}
				else
				{
					var point1 = line[pointIndex][0];
					var point2 = line[pointIndex + 1][0];
					var startPoint = point1 < point2 ? point1 : point2;
					var endPoint = point1 < point2 ? point2 : point1;
					for (var i = startPoint; i <= endPoint; i++)
					{
						grid[line[pointIndex][1]][i - _xOffset] = '#';
					}
				}
			}
		}

		var currentSandPoint = (x: 500, y: 1);
		var sandCount = 0;
		while (true)
		{
			// Console.WriteLine(Extensions.ToString(grid));
			var fallLocation = CalculateFallLocation(currentSandPoint, grid);
			if (!fallLocation.HasValue)
			{
				currentSandPoint = (500, 1);
				sandCount++;
				continue;
			}

			if (fallLocation.Value == (-1, -1))
			{
				return $"{sandCount}";
			}

			grid[fallLocation.Value.y][fallLocation.Value.x] = 'o';
			grid[currentSandPoint.y][currentSandPoint.x - _xOffset] = '.';
			currentSandPoint = (fallLocation.Value.x + _xOffset, fallLocation.Value.y);
		}
	}

	private (int x, int y)? CalculateFallLocation((int x, int y) currentSandPoint, IReadOnlyList<char[]> grid)
	{
		if (currentSandPoint.y == grid.Count - 1)
		{
			return (-1, -1);
		}

		var below = grid[currentSandPoint.y + 1][currentSandPoint.x - _xOffset];

		if (below == '.')
		{
			return (currentSandPoint.x - _xOffset, currentSandPoint.y + 1);
		}

		if (currentSandPoint.x - _xOffset == 0)
		{
			return (-1, -1);
		}

		var belowLeft = grid[currentSandPoint.y + 1][currentSandPoint.x - _xOffset - 1];

		if (belowLeft == '.')
		{
			return (currentSandPoint.x - _xOffset - 1, currentSandPoint.y + 1);
		}

		if (currentSandPoint.x - _xOffset == grid[0].Length - 1)
		{
			return (-1, -1);
		}

		var belowRight = grid[currentSandPoint.y + 1][currentSandPoint.x - _xOffset + 1];

		if (belowRight == '.')
		{
			return (currentSandPoint.x - _xOffset + 1, currentSandPoint.y + 1);
		}

		return null;
	}
}