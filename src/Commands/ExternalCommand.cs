using System.Diagnostics;
using src.ShellTokenizer;

namespace src.Commands;

public class ExternalCommand :  ICommand
{
    public void Execute(List<Token> tokens)
    {
        if (!PathVariable.TryGet(tokens[0].Value, out var path)) return;
        
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
            processInfo.ArgumentList.Add(token.Value); // No manual quoting!
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
}