using src;
using System.Diagnostics;

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
        else if (tokens[0] == Commands.Cd)
        {
            if (!Directory.Exists(string.Join(' ', tokens.Skip(1))) && tokens[1] != "~")
            {
                Console.WriteLine($"cd: {string.Join(' ', tokens.Skip(1))}: No such file or directory");
            }
            else
            {
                path = tokens.Skip(1).First();

                if (path == "~")
                {
                    Directory.SetCurrentDirectory(Environment.GetEnvironmentVariable("HOME") ?? string.Empty);
                }
                else
                {
                    Directory.SetCurrentDirectory(path);
                }
            }
        }
    }
    else if (PathVariable.TryGet(tokens[0], out path)) // path of an exe file, I want to run it
    {
        var processInfo = new ProcessStartInfo
        {
            FileName = Path.GetFileName(path),
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        // Add all file arguments at once
        foreach (var token in tokens.Skip(1))
        {
            processInfo.ArgumentList.Add(token); // No manual quoting!
        }

        using var process = Process.Start(processInfo);
        var output = process?.StandardOutput.ReadToEnd();
        var error = process?.StandardError.ReadToEnd();
        process?.WaitForExit();

        if (!string.IsNullOrEmpty(output))
            Console.Write(output);
        if (!string.IsNullOrEmpty(error))
            Console.Error.Write(error);
    }
    else Console.WriteLine($"{command}: command not found");
}