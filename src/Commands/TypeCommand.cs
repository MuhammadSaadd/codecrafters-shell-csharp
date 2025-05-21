using src.ShellTokenizer;

namespace src.Commands;

public class TypeCommand  : ICommand
{
    public void Execute(List<Token> tokens)
    {
        string output;

        if (CommandsEnum.Map.Contains(tokens[1].Value))
        {
            output = $"{tokens[1]} is a shell builtin";
        }
        else
        {
            output = PathVariable.TryGet(tokens[1].Value, out var path)
                ? $"{tokens[1]} is {path}"
                : $"{tokens[1]}: not found";
        }

        Console.WriteLine(output);
    }
}