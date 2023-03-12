using ConsoleApp.Commands;

namespace ConsoleApp;

public class CommandHandler
{
    public static void ExecuteCommand(ICommand command)
    {
        command.Execute();
    }
}