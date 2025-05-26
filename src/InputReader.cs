using System.Text;

namespace src;

public static class InputReader
{
    public static string Read()
    {
        var keyBuffer = new StringBuilder();

        while (true)
        {
            var key = Console.ReadKey(intercept: true);

            if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                break;
            }

            switch (key.Key)
            {
                case ConsoleKey.Tab:
                {
                    var prefix = keyBuffer.ToString();

                    var matches = Startup.Commands.GetWords(prefix);
                    var match = matches.FirstOrDefault();
                    
                    if (string.IsNullOrEmpty(match))
                    {
                        Console.Beep();
                    }
                    else
                    {
                        Console.Write(match[prefix.Length..] + " ");

                        keyBuffer.Append(match[prefix.Length..] + " ");
                    }

                    break;
                }
                case ConsoleKey.Backspace when keyBuffer.Length > 0:
                    Console.Write("\b \b");

                    keyBuffer.Length--;
                    break;
                default:
                {
                    if (!char.IsControl(key.KeyChar))
                    {
                        Console.Write(key.KeyChar);

                        keyBuffer.Append(key.KeyChar);
                    }

                    break;
                }
            }
        }

        return keyBuffer.ToString();
    }
}