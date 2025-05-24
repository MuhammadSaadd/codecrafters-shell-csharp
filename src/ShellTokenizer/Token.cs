namespace src.ShellTokenizer;

public record Token
{
    public TokenType Type { get; init; }
    public required string Value { get; init; }
    
    public override string ToString()
    {
        return $"{Type}: '{Value}'";
    }
}