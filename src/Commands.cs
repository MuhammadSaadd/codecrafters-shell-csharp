namespace src;

public static class Commands
{
    public static string Exit => "exit";
    public static string Echo => "echo";
    public static string Type => "type";
    public static string Pwd => "pwd";
    public static string Cd => "cd";

    public static readonly HashSet<string> Map =
    [
        Exit,
        Echo,
        Type,
        Pwd,
        Cd
    ];
}