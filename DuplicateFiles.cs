using System.Collections;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace Duped;
public static class DuplicateFiles
{
    private const int MaxCap = 1024;
    private static readonly List<string> DirectoryStorage = new(MaxCap);
    private static int _size;
    private static readonly Hashtable Hashtable = new();
    public static void Visit_files_recursively(string dirPath)
    {
        Debug.Assert(dirPath != null, "Directory path should not be null");
        var fAttr = File.GetAttributes(dirPath);
        if (fAttr.HasFlag(FileAttributes.Directory))
        {
            Debug.Assert(MaxCap > 0, "List size must be greater than zero");
           DirectoryStorage.Add(dirPath);
           _size++;
        }

        var files = Directory.GetFiles(dirPath);
        foreach (var file in files)
        {
            Hashtable.Add(HashOfFile(file), file);
        }
        var subdirs = Directory.GetDirectories(dirPath);
        foreach (var dir in subdirs)
        {
            Visit_files_recursively(dir);
        }
    }

    public static void PrintHashTable()
    {
        foreach (var res in Hashtable)
        {
            Console.WriteLine("Entry :- {0}",res);
        }
        Console.WriteLine($"Total number of entries in hashtable is {Hashtable.Count}");
    }

    private static string HashOfFile(string input)
    {
        var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    }
}