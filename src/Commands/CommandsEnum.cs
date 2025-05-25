namespace src.Commands;

public static class CommandsEnum
{
    public const string Echo = "echo";
    public const string Type = "type";
    public const string Pwd = "pwd";
    public const string Exit = "exit";
    public const string Cd = "cd";

    public static readonly HashSet<string> Map =
    [
        Exit,
        Echo,
        Type,
        Pwd,
        Cd
    ];
}