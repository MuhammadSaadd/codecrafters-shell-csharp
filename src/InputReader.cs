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
                Console.WriteLine();
                break;
            }

            switch (key.Key)
            {
                case ConsoleKey.Tab:
                {
                    var prefix = keyBuffer.ToString();

                    var matches = Startup.Commands
                        .GetWords(prefix)
                        .Order()
                        .ToList();

                    switch (matches.Count)
                    {
                        case 0:
                        case > 1 when lastKey != ConsoleKey.Tab:
                        {
                            var lcp = LongestCommonPrefix(matches);

                            if (lcp.Length <= prefix.Length)
                            {
                                Console.Beep();
                            }
                            else
                            {
                                var append = lcp.AsSpan(keyBuffer.Length).ToArray();

                                foreach (var t in append)
                                {
                                    Console.Write(t);
                                    keyBuffer.Append(t);
                                }
                            }
                            
                            break;
                        }
                        case > 1:
                        {
                            Console.WriteLine();
                            Console.WriteLine(string.Join("  ", matches));
                            Console.Write($"$ {keyBuffer}");
                            break;
                        }
                        default:
                        {
                            var match = matches[0];

                            Console.Write(match[prefix.Length..] + " ");

                            keyBuffer.Append(match[prefix.Length..] + " ");
                            break;
                        }
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

    private static string LongestCommonPrefix(List<string> matches)
    {
        if (matches.Count == 0) return string.Empty;
        var prefix = matches[0];

        foreach (var match in matches)
        {
            while (!match.StartsWith(prefix))
            {
                prefix = prefix.AsSpan(0, prefix.Length - 1).ToString();

                if (prefix == "")
                {
                    return "";
                }
            }
        }

        return prefix;
    }
}