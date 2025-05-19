using System.Diagnostics;
using src;

while (true)
{
    #region Vars

    string? path;

    #endregion

    Console.Write("$ ");

    var command = Console.ReadLine();

    var tokens = Tokenizer.Tokens(command);

    if (tokens.Count == 0) Console.WriteLine($"{command}: command not found");
    else if (Commands.Map.Contains(tokens[0]))
    {
        if (tokens[0] == Commands.Exit)
        {
            if (tokens[1] == "0") Environment.Exit(0);
        }
        else if (tokens[0] == Commands.Echo)
        {
            var output = new string[tokens.Count - 1];

            for (var i = 1; i < tokens.Count; i++)
            {
                output[i - 1] = tokens[i];
            }

            Console.WriteLine(string.Join(' ', output));
        }
        else if (tokens[0] == Commands.Type)
        {
                string output;

                if (Commands.Map.Contains(tokens[1]))
                {
                    output = $"{tokens[1]} is a shell builtin";
                }
                else
                {
                    output = PathVariable.TryGet(tokens[1], out path)
                        ? $"{tokens[1]} is {path}"
                        : $"{tokens[1]}: not found";
                }

                Console.WriteLine(output);
        }
        else if (tokens[0] == Commands.Pwd)
        {
            Console.WriteLine(Directory.GetCurrentDirectory());
        }
    }
    else if (PathVariable.TryGet(tokens[0], out path)) // path of an exe file, I want to run it
    {
        var arguments = string.Join(' ', tokens.Skip(1).ToArray());
        
        var processInfo = new ProcessStartInfo
        {
            FileName = Path.GetFileName(path),
            Arguments = arguments,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };

        using var process = Process.Start(processInfo);
        var output = process?.StandardOutput.ReadToEnd();
        process?.WaitForExit();
        Console.Write(output);
    }
    else Console.WriteLine($"{command}: command not found");
}