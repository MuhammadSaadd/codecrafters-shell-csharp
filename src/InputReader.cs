using System.Text;

namespace src;

public static class InputReader
{
    public static string Read()
    {
        ConsoleKey? lastKey = null;
        var keyBuffer = new StringBuilder();

        while (true)
        {
            var key = Console.ReadKey(intercept: true);

            if (key.Key == ConsoleKey.Enter)
            {
                lastKey = null;
                Console.WriteLine();
                break;
            }

            switch (key.Key)
            {
                case ConsoleKey.Tab:
                {
                    var prefix = keyBuffer.ToString();

                    var matches = Startup.Commands.GetWords(prefix).ToList();
                    
                    if (matches.Count == 0)
                    {
                        Console.Beep();
                    }
                    else if(matches.Count > 1)
                    {
                        if(lastKey != ConsoleKey.Tab) Console.Beep();
                        else
                        {
                            Console.WriteLine();
                            foreach (var match in matches.Order())
                            {
                                Console.Write($"{match}  ");
                            }
                            
                            Console.WriteLine();
                            Console.Write($"$ {keyBuffer}");
                        }
                    }
                    else
                    {
                        var match = matches[0];
                        
                        Console.Write(match[prefix.Length..] + " ");

                        keyBuffer.Append(match[prefix.Length..] + " ");
                    }

                    lastKey = ConsoleKey.Tab;
                    break;
                }
                case ConsoleKey.Backspace when keyBuffer.Length > 0:
                    Console.Write("\b \b");
                    lastKey = null;
                    keyBuffer.Length--;
                    break;
                default:
                {
                    lastKey = null;
                        
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