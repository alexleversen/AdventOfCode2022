namespace AdventOfCode2022.Models.Day7;

public class Directory : IFileSystemObject
{
    public Directory(string name, Directory? parent)
    {
        Name = name;
        Parent = parent;
        ChildDirectories = new List<Directory>();
        ChildFiles = new List<File>();
    }

    public bool IsDirectory => true;

    public int Size => new List<IFileSystemObject>()
        .Concat(ChildDirectories)
        .Concat(ChildFiles)
        .Select(o => o.Size)
        .Sum();
    public string Name { get; }
    public Directory? Parent { get; }
    public List<Directory> ChildDirectories { get; }
    public List<File> ChildFiles { get; }
}