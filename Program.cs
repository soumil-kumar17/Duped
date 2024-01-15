using Duped;

Console.WriteLine("Enter directory  name :- ");
var dirPath = Console.ReadLine();
DuplicateFiles.Visit_files_recursively(dirPath!);
