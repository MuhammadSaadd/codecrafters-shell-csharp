using src;
using src.Commands;
using src.ShellTokenizer;


Startup.Build();

while (true)
{
    Console.Write("$ ");

    var input = InputReader.Read();

    var tokenizer = new Tokenizer(input);
    var tokens = tokenizer.TokenizeAll();
    var parserInfo = Parser.Parse(tokens);

    if (parserInfo.Tokens.Count == 0)
    {
        Console.WriteLine($"{input}: command not found");
        continue;
    }

    ICommand? command = null;
    var cmdValue = parserInfo.Tokens[0].Value;

    if (CommandsEnum.Map.Contains(cmdValue))
    {
        command = cmdValue switch
        {
            CommandsEnum.Exit => new ExitCommand(),
            CommandsEnum.Echo => new EchoCommand(),
            CommandsEnum.Type => new TypeCommand(),
            CommandsEnum.Pwd => new PwdCommand(),
            CommandsEnum.Cd => new CdCommand(),
            _ => null
        };
    }
    else if (PathVariablesRepository.TryGet(cmdValue, out var path))
    {
        command = new ExternalCommand(path!);
    }

    if (command is not null)
    {
        Commander.Invoke(
            command,
            parserInfo.Tokens,
            parserInfo.OutputFile,
            parserInfo.ErrorFile,
            parserInfo.AppendOutput,
            parserInfo.AppendError);
    }
    else
    {
        Console.WriteLine($"{input}: command not found");
    }
}