using System.Net;
using System.Net.Sockets;


// Wait for user input
while (true)
{
    Console.Write("$ ");

    var command = Console.ReadLine();

    System.Console.WriteLine($"{command}: command not found");
}
