using src;
using src.Commands;
using src.ShellTokenizer;

while (true)
{
    Console.Write("$ ");

    var input = Console.ReadLine();

    var tokenizer = new Tokenizer(input);

    var tokens = tokenizer.TokenizeAll();
    var parserInfo = Parser.Parse(tokens);

    if (parserInfo.Tokens.Count == 0) Console.WriteLine($"{input}: command not found");
    else if (CommandsEnum.Map.Contains(parserInfo.Tokens[0].Value))
    {
        ICommand command = null!;

        if (parserInfo.Tokens[0].Value == CommandsEnum.Exit) command = new ExitCommand();
        else if (parserInfo.Tokens[0].Value == CommandsEnum.Echo) command = new EchoCommand();
        else if (parserInfo.Tokens[0].Value == CommandsEnum.Type) command = new TypeCommand();
        else if (parserInfo.Tokens[0].Value == CommandsEnum.Pwd) command = new PwdCommand();
        else if (parserInfo.Tokens[0].Value == CommandsEnum.Cd) command = new CdCommand();

        Commander.Invoke(
            command,
            parserInfo.Tokens,
            parserInfo.OutputFile,
            parserInfo.ErrorFile,
            parserInfo.AppendOutput,
            parserInfo.AppendError);
    }
    else if (PathVariablesRepository.TryGet(parserInfo.Tokens[0].Value, out var path)) // path of an exe file, I want to run it
    {
        var command = new ExternalCommand(path!);

        Commander.Invoke(
            command,
            parserInfo.Tokens,
            parserInfo.OutputFile,
            parserInfo.ErrorFile,
            parserInfo.AppendOutput,
            parserInfo.AppendError);
    }
    else Console.WriteLine($"{input}: command not found");
}