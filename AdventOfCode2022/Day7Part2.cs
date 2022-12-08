using System.Text.RegularExpressions;
using AdventOfCode2022.Models.Day7;
using Directory = AdventOfCode2022.Models.Day7.Directory;
using File = AdventOfCode2022.Models.Day7.File;

namespace AdventOfCode2022;

public class Day7Part2 : IPuzzleSolver
{
    private const int TotalDiskSize = 70000000;
    private const int RequiredSpace = 30000000;
    private static readonly Regex CdRegex = new("^\\$ cd ([a-z.]+)$");
    private static readonly Regex DirRegex = new("^dir ([a-z.]+)$");
    private static readonly Regex FileRegex = new("^(\\d+) ([a-z.]+)$");
    public string Run(string[] input)
    {
        var fileSystem = ParseFileSystem(input);
        var usedSpace = fileSystem.Size;
        var minSpaceToDelete = usedSpace + RequiredSpace - TotalDiskSize;

        return $"{CalculateDirectorySizeToDelete(fileSystem, minSpaceToDelete)}";
    }

    private static int CalculateDirectorySizeToDelete(IFileSystemObject fileSystemObject, int minSpaceToDelete)
    {
        var currentSize = fileSystemObject.Size;
        if (currentSize > minSpaceToDelete)
        {
            if (fileSystemObject is not Directory currentDir)
            {
                return currentSize;
            }

            return new[] { currentSize }
                .Concat(currentDir.ChildDirectories.Select(d => CalculateDirectorySizeToDelete(d, minSpaceToDelete)))
                .Concat(currentDir.ChildFiles.Select(f => CalculateDirectorySizeToDelete(f, minSpaceToDelete)))
                .Min();
        }

        return int.MaxValue;
    }

    private static Directory ParseFileSystem(string[] input)
    {
        var rootDir = new Directory("/", null);
        var currentDir = rootDir;
        foreach (var line in input[1..])
        {
            var cdMatch = CdRegex.Match(line);
            var dirMatch = DirRegex.Match(line);
            var fileMatch = FileRegex.Match(line);
            if (cdMatch.Success)
            {
                currentDir = cdMatch.Groups[1].Value == ".."
                    ? currentDir?.Parent
                    : currentDir?.ChildDirectories.First(dir => dir.Name == cdMatch.Groups[1].Value);
            }
            else if (dirMatch.Success)
            {
                currentDir?.ChildDirectories.Add(new Directory(dirMatch.Groups[1].Value, currentDir));
            }
            else if (fileMatch.Success)
            {
                currentDir?.ChildFiles.Add(
                    new File(
                        fileMatch.Groups[2].Value,
                        int.Parse(fileMatch.Groups[1].Value),
                        currentDir
                    )
                );
            }
        }

        return rootDir;
    }
}