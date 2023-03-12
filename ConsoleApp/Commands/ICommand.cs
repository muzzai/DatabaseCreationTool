using DataAccessLayer;

namespace ConsoleApp.Commands;

public interface ICommand
{
    public abstract void Execute();
}