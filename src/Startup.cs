using rm.Trie;
using src.Commands;

namespace src;

public static class Startup
{
    public static Trie Commands { get; set; } = new();

    public static void Build()
    {
        var externalCommands = PathVariablesRepository.GetAllCommands();
        var allCommands = externalCommands.Concat(CommandsEnum.Map).ToList();

        foreach (var command in allCommands)
        {
            Commands.AddWord(command);
        }
    }
}