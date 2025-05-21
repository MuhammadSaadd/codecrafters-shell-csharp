using src;
using src.Commands;
using src.ShellTokenizer;

while (true)
{
    Console.Write("$ ");

    var input = Console.ReadLine();

    var tokenizer = new Tokenizer(input);

    var tokens = tokenizer.TokenizeAll();

    if (tokens.Count == 0) Console.WriteLine($"{input}: command not found");
    else if (CommandsEnum.Map.Contains(tokens[0].Value))
    {
        if (tokens[0].Value == CommandsEnum.Exit)
        {
            var command = new ExitCommand();
            Commander.Invoke(command, tokens);
            // var output =  outputWriter.ToString();
            // var error = errorWriter.ToString();
        }
        else if (tokens[0].Value == CommandsEnum.Echo)
        {
            var command = new EchoCommand();
            Commander.Invoke(command, tokens);
        }
        else if (tokens[0].Value == CommandsEnum.Type)
        {
            var command = new TypeCommand();
            Commander.Invoke(command, tokens);
        }
        else if (tokens[0].Value == CommandsEnum.Pwd)
        {
            var command = new PwdCommand();
            Commander.Invoke(command, tokens);
        }
        else if (tokens[0].Value == CommandsEnum.Cd)
        {
            var command = new CdCommand();
            Commander.Invoke(command, tokens);
        }
    }
    else if (PathVariable.TryGet(tokens[0].Value, out var path)) // path of an exe file, I want to run it
    {
    }
    else Console.WriteLine($"{input}: command not found");
}