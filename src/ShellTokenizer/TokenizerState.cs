namespace src.ShellTokenizer;

/// <summary>
/// The different states that the tokenizer can be in.
/// </summary>
public enum TokenizerState
{
    Normal,         // Regular processing
    SingleQuote,    // Inside single quotes
    DoubleQuote,    // Inside double quotes
    Escape,         // After backslash
    EscapeInDouble, // Escape inside double quotes
    Variable,       // Processing variable name
    VariableBrace,  // Processing variable name with braces
    Operator        // Processing an operator
}