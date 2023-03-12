using ConsoleApp;



const string appDescription = @"
    This is a database creation tool
    It connects to a postgres instance and creates a database with three tables 
";

List<string> commandList = new List<string>()
{
    "Print",
    "AddObject",
    "Exit"
}; 

var commandsDescription = @"
Commands are:
Print - prints all tables
AddObject - Adds Object
Exit - exit 
";

Console.WriteLine(appDescription);
Console.WriteLine(commandsDescription);
await Helper.AddEntities(10);

while (true)
{
    var userInput = Console.ReadLine();
    if (userInput == "exit") return;
    if (!commandList.Contains(userInput))
    {
        Console.WriteLine(commandsDescription);
        continue;
    };

    try
    {
        var command = CommandFactory.CreateCommand(userInput);
        CommandHandler.ExecuteCommand(command);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }
} 




