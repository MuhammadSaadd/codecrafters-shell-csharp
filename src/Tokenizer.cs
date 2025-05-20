using System.Text;

namespace src;

// echo
public static class Tokenizer
{
    public static List<string> Tokens(string? command)
    {
        if (command == null) return [];

        List<string> tokens = [];

        Stack<char> quotes = [];
        StringBuilder token = new();

        var i = 0;
        while (i < command.Length)
        {
            if (command[i] == '\'' || command[i] == '\"')
            {
                quotes.Push(command[i]);
                i++;

                while (i < command.Length)
                {
                    if (command[i] == quotes.Peek()) // '   ''
                    {
                        if (i + 1 < command.Length && command[i] == command[i + 1])
                        {
                            i += 2;
                        }
                        else break;
                    }
                    else
                    {
                        token.Append(command[i]);
                        i++;
                    }
                }

                if (i < command.Length) i++;
                quotes.Pop();
            }
            else
            {
                while (i < command.Length && command[i] != ' ')
                {
                    token.Append(command[i]);
                    i++;
                }
            }

            tokens.Add(token.ToString());
            token.Clear();

            // Skip any spaces between tokens
            while (i < command.Length && command[i] == ' ')
            {
                i++;
            }
        }

        if (!string.IsNullOrEmpty(token.ToString())) tokens.Add(token.ToString());

        return tokens;
    }
}