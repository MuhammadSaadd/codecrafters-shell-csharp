using System.Text;

namespace src;

public static class Tokenizer
{
    public static List<string> Tokens(string? command)
    {
        List<string> tokens = [];

        //echo hello world
        StringBuilder token = new();

        for (var i = 0; i < command?.Length; i++)
        {
            if (command[i] == ' ')
            {
                tokens.Add(token.ToString());
                token.Clear();
            }
            else
            {
                token.Append(command[i]);
            }
        }

        if (!string.IsNullOrEmpty(token.ToString())) tokens.Add(token.ToString());

        return tokens;
    }
}