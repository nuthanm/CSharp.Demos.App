//Objective: User input is going to add it in .txt file
var directory = "c://temp";
Console.WriteLine("Hello Friend!!!");
Console.WriteLine("Please add your input:");
var userInput = Console.ReadLine();

if (string.IsNullOrEmpty(userInput) || string.IsNullOrWhiteSpace(userInput))
{
    Console.WriteLine("Input is mandatory to proceed further. Please add your input.");
    return;
}

// Write the user input in .txt file
try
{
    // Input : c://temp//filename.txt or c:\\temp\\filename.txt
    // Exception: System.IO.DirectoryNotFoundException: 'Could not find a part of the path 'c:\temp\filename.txt'.'
    // Fix for this issue : create this folder and file before you run the following code.

    // Other option: 
    /*
     * Step 1 : Folder exists if not create it
     * Step 2 : File exists if not create it 
     * Step 3: Call the below code to avoid unhandled exceptions
     */

    // File is always overwrite with new input based out of the following code.
    var isFolderExists = Directory.Exists(path: directory);
    if (!isFolderExists)
    {
        Directory.CreateDirectory(directory);
    }

    var isFileExists = File.Exists($"{directory}\\filename.txt");
    if(!isFileExists)
    {
        File.Create($"{directory}\\filename.txt");
    }

    // The following code always overwrite the file and to make this code to append the new text then add second parameter as true
    using (StreamWriter writer = new StreamWriter($"{directory}\\filename.txt", true))
    {
        writer.WriteLine(userInput);
    }
}
catch (IOException e)
{
    Console.WriteLine("An error occurred while writing to the file: " + e.Message);
    return;
}
catch (Exception e)
{
    Console.WriteLine("An unexpected error occurred: " + e.Message);
    return;
}

Console.WriteLine("Thank you friend!!! we catchup later.");
Console.Read();

