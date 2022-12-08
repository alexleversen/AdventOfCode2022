using System.Text.RegularExpressions;
using AdventOfCode2022.Models.Day7;
using Directory = AdventOfCode2022.Models.Day7.Directory;
using File = AdventOfCode2022.Models.Day7.File;

namespace AdventOfCode2022;

public class Day7Part1 : IPuzzleSolver
{
    private const int MaxDirectorySize = 100000;
    private static readonly Regex CdRegex = new("^\\$ cd ([a-z.]+)$");
    private static readonly Regex DirRegex = new("^dir ([a-z.]+)$");
    private static readonly Regex FileRegex = new("^(\\d+) ([a-z.]+)$");
    public string Run(string[] input)
    {
        var fileSystem = ParseFileSystem(input);

        return $"{SumSizes(fileSystem)}";
    }

    private static int SumSizes(IFileSystemObject fileSystemObject)
    {
        var currentSize = fileSystemObject.Size;
        if (currentSize > MaxDirectorySize)
        {
            currentSize = 0;
        }

        if (fileSystemObject is Directory directory)
        {
            return currentSize
                   + directory.ChildDirectories.Sum(SumSizes);
        }

        return currentSize;
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