using src;

while (true)
{
    Console.Write("$ ");

    var command = Console.ReadLine();

    var tokens = Tokenizer.Tokens(command);

    if (tokens.Count == 0 || !Commands.Map.Contains(tokens[0]))
        Console.WriteLine($"{command}: command not found");
    else
    {
        if (tokens[0] == Tokenizer.Tokens(Commands.Exit)[0])
        {
            if(tokens[1] == "0") Environment.Exit(0);
        }
        else if (tokens[0] == Tokenizer.Tokens(Commands.Echo)[0])
        {
            var output = new string[tokens.Count - 1];

            for (var i = 1; i < tokens.Count; i++)
            {
                output[i - 1] = tokens[i];
            }
            Console.WriteLine(string.Join(' ', output));
        }
    }
}