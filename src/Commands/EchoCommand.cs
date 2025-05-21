using src.ShellTokenizer;

namespace src.Commands;

public class EchoCommand :  ICommand
{
    public void Execute(List<Token> tokens)
    {
        var output = new string[tokens.Count - 1];

        for (var i = 1; i < tokens.Count; i++)
        {
            output[i - 1] = tokens[i].Value;
        }

        Console.WriteLine(string.Join(' ', output));
    }
}