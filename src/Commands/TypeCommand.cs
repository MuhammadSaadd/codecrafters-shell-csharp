using src.ShellTokenizer;

namespace src.Commands;

public class TypeCommand  : ICommand
{
    public void Execute(List<Token> tokens)
    {
        string output;

        if (CommandsEnum.Map.Contains(tokens[1].Value))
        {
            output = $"{tokens[1].Value} is a shell builtin";
        }
        else
        {
            output = PathVariable.TryGet(tokens[1].Value, out var path)
                ? $"{tokens[1].Value} is {path}"
                : $"{tokens[1].Value}: not found";
        }

        Console.WriteLine(output);
    }
}