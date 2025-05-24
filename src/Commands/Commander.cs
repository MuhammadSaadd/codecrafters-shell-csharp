using src.ShellTokenizer;

namespace src.Commands;

public static class Commander
{
    public static void Invoke(
        ICommand command,
        List<Token> tokens,
        string? outputFile,
        string? errorFile,
        bool appendOutput,
        bool appendError)
    {
        if (outputFile != null)
        {
            if (!File.Exists(outputFile))
            {
                var file = File.Create(outputFile);
                file.Close();
            }
            
            if (!appendOutput)
            {
                File.WriteAllText(outputFile, string.Empty);
            }
        }

        if (errorFile != null)
        {
            if (!File.Exists(errorFile))
            {
                var file = File.Create(errorFile);
                file.Close();
            }
            
            if (!appendError)
            {
                File.WriteAllText(errorFile, string.Empty);
            }
        }

        using TextWriter outputWriter =
            string.IsNullOrEmpty(outputFile) ? new StringWriter() : new StreamWriter(outputFile, appendOutput);
        using TextWriter errorWriter =
            string.IsNullOrEmpty(errorFile) ? new StringWriter() : new StreamWriter(errorFile, appendError);

        var originalOut = Console.Out;
        var originalError = Console.Error;

        Console.SetOut(outputWriter);
        Console.SetError(errorWriter);

        try
        {
            Task.Run(() => command.Execute(tokens)).Wait();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Exception: " + ex.Message);
        }
        finally
        {
            outputWriter.Flush();
            errorWriter.Flush();

            Console.SetOut(originalOut);
            Console.SetError(originalError);
        }

        if (outputFile == null && outputWriter is StringWriter stringOutput)
        {
            Console.Write(stringOutput.ToString());
        }

        if (errorFile != null || errorWriter is not StringWriter stringError) return;

        var errorContent = stringError.ToString();
        if (!string.IsNullOrEmpty(errorContent))
        {
            Console.Error.Write(errorContent);
        }
    }
}
