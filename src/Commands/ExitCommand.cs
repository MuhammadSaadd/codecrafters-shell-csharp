using src.ShellTokenizer;

namespace src.Commands;

public class ExitCommand : ICommand
{
    public void Execute(List<Token> tokens)
    {
        if (tokens[1].Value == "0") Environment.Exit(0);
    }
}