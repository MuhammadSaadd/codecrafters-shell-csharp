using src.ShellTokenizer;

namespace src.Commands;

public class PwdCommand :  ICommand
{
    public void Execute(List<Token> tokens)
    {
        Console.WriteLine(Directory.GetCurrentDirectory());
    }
}