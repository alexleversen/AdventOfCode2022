namespace AdventOfCode2022.Models.Day7;

public class File : IFileSystemObject
{
    public File(string name, int size, Directory parent)
    {
        Parent = parent;
        Size = size;
        Name = name;
    }
    public bool IsDirectory => false;
    public string Name { get; }
    public int Size { get; }
    public Directory Parent { get; set; }
}