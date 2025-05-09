using System.Net;
using System.Net.Sockets;

HashSet<string> commands = [
    "exit 0"
];


while (true)
{
    Console.Write("$ ");

    var command = Console.ReadLine();

    if (string.IsNullOrEmpty(command) || !commands.Contains(command))
        System.Console.WriteLine($"{command}: command not found");
    else
    {
        if (command == "exit 0")
        {
            Environment.Exit(0);
        }
    }
}


