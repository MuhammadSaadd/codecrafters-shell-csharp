using src.ShellTokenizer;

namespace src.Commands;

public class CdCommand : ICommand
{
    public void Execute(List<Token> tokens)
    {
        if (!Directory.Exists(string.Join(' ', tokens.Skip(1).Select(t => t.Value).FirstOrDefault())) && tokens[1].Value != "~")
        {
            Console.WriteLine($"cd: {string.Join(' ', tokens.Skip(1).Select(t => t.Value))}: No such file or directory");
        }
        else
        {
            var path = tokens.Skip(1).First();

            if (path.Value == "~")
            {
                Directory.SetCurrentDirectory(Environment.GetEnvironmentVariable("HOME") ?? string.Empty);
            }
            else
            {
                Directory.SetCurrentDirectory(path.Value);
            }
        }
    }
}