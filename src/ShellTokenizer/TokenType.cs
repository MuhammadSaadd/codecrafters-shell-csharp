namespace src.ShellTokenizer;

/// <summary>
/// Represents different types of tokens in a shell command.
/// </summary>
public enum TokenType
{
    Word,           // Commands, arguments, etc.
    Operator,       // |, >, >>, <, etc.
    Variable,       // $var, ${var}
    CommandSubst,   // $(command) or `command`
    Eof             // End of input
}