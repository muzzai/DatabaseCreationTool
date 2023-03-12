using ConsoleApp.Commands;
using DataAccessLayer;

namespace ConsoleApp;

public static class CommandFactory
{
    public static ICommand? CreateCommand(string commandName)
    {
        return commandName switch
        {
            "AddObject" => new AddObjectCommand(),
            "Print" => new PrintCommand(),
            _ => null
        };
    }
}