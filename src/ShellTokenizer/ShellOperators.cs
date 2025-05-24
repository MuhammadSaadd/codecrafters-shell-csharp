namespace src.ShellTokenizer;

public static class ShellOperators
{
    public static string Pipe => "|";
    public static string OutputRedirect => ">";
    public static string StdoutRedirect => "1>";
    public static string OutputAppend => ">>"; // 2>> | 1>>
    public static string StdOutputAppend => "1>>";
    public static string StderrRedirect => "2>";
    public static string StderrAppend => "2>>";
    public static string InputRedirect => "<";
    public static string HereDocument => "<<";
    public static string LogicalAnd => "&&";
    public static string LogicalOr => "||";
    public static string Sequential => ";";
    public static string Background => "&";


    public static readonly HashSet<string> All =
    [
        Pipe,
        OutputRedirect,
        StdoutRedirect,
        StderrRedirect,
        OutputAppend,
        InputRedirect,
        HereDocument,
        LogicalAnd,
        LogicalOr,
        Sequential,
        Background
    ];
    
    public static readonly HashSet<string> Redirections =
    [
        OutputRedirect,
        StdoutRedirect,
        StderrRedirect,
        OutputAppend,
        InputRedirect,
        HereDocument
    ];
    
    public static readonly HashSet<string> OutputRedirections =
    [
        OutputRedirect,
        StdoutRedirect,
        StderrRedirect,
        OutputAppend
    ];
    
    public static readonly HashSet<string> InputRedirections =
    [
        InputRedirect,
        HereDocument
    ];
    
    // public static readonly HashSet<string> LogicalOperators =
    // [
    //     LogicalAnd,
    //     LogicalOr
    // ];
    
    // public static readonly HashSet<string> ExecutionControl =
    // [
    //     LogicalAnd,
    //     LogicalOr,
    //     Sequential,
    //     Background
    // ];
    
    public static bool IsOperator(string token) => All.Contains(token);
    
    public static bool IsRedirection(string token) => Redirections.Contains(token);
    
    public static bool IsOutputRedirection(string token) => OutputRedirections.Contains(token);
    
    public static bool IsInputRedirection(string token) => InputRedirections.Contains(token);
    
    // public static bool IsLogicalOperator(string token) => LogicalOperators.Contains(token);

    // public static bool IsExecutionControl(string token) => ExecutionControl.Contains(token);
}