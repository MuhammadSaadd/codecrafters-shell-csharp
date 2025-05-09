using System.Net;
using System.Net.Sockets;

// Uncomment this line to pass the first stage
Console.Write("$ ");

// Wait for user input
while (true)
{
    var command = Console.ReadLine();

    System.Console.WriteLine($"{command}: command not found");
}
