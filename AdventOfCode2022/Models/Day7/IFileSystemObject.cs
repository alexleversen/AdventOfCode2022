namespace AdventOfCode2022.Models.Day7;

public interface IFileSystemObject
{
    public string Name { get; }
    public bool IsDirectory { get; }
    public int Size { get; }
    public Directory? Parent { get; }
}