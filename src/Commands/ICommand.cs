using src.ShellTokenizer;

namespace src.Commands;

public interface ICommand
{
    void Execute(List<Token> tokens);
}