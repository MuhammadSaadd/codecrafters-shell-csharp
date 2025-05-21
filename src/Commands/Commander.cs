using src.ShellTokenizer;

namespace src.Commands;

public static class Commander
{
    public static void Invoke(ICommand command, List<Token> tokens)
    {
        using var outputWriter = new StringWriter();
        using var errorWriter = new StringWriter();

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
            Console.SetOut(originalOut);
            Console.SetError(originalError);
        }

        Console.Write(outputWriter.ToString());
        // Console.WriteLine(errorWriter.ToString());
        // var output =  outputWriter.ToString();
        // var error = errorWriter.ToString();
    }
}