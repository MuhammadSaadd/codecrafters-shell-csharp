using System.Text;

namespace src.ShellTokenizer;

/// <summary>
/// A state-machine-based tokenizer
/// </summary>
public class Tokenizer(string? input)
{
    private int _position;
    private char? _specialNumber;
    private readonly HashSet<char> _escapeInDoubleChars = ['\\', '"', '$', '`', '\n'];
    
    /// <summary>
    /// Returns the next character without advancing the position.
    /// </summary>
    private char Peek()
    {
        return input != null && _position >= input.Length ? '\0' : input![_position];
    }

    /// <summary>
    /// Returns the next character and advances the position.
    /// </summary>
    private char Next()
    {
        return input != null && _position >= input.Length ? '\0' : input![_position++];
    }

    /// <summary>
    /// Returns the next character
    /// </summary>
    private char ReadNext()
    {
        var next = _position + 1;
        return input != null && next >= input.Length ? '\0' : input![next];
    }

    /// <summary>
    /// Checks if the current character is whitespace.
    /// </summary>
    private static bool IsWhitespace(char c)
    {
        return c is ' ' or '\t' or '\n' or '\r';
    }

    /// <summary>
    /// Checks if the character could be part of an operator.
    /// </summary>
    private static bool IsSpecialNumber(char c)
    {
        var num = c - '0';
        
        return num is 1 or 2;
    }

    /// <summary>
    /// Returns the next token from the input.
    /// </summary>
    private Token NextToken()
    {
        // Skip whitespace
        while (input != null && _position < input.Length && IsWhitespace(Peek()))
        {
            _position++;
        }

        // Check for the end of input
        if (input != null && _position >= input.Length)
        {
            return new Token
            {
                Type = TokenType.Eof,
                Value = string.Empty
            };
        }

        var state = TokenizerState.Normal;
        var tokenBuilder = new StringBuilder();
        var tokenType = TokenType.Word;

        while (input != null && _position < input.Length)
        {
            var c = Peek();

            switch (state)
            {
                case TokenizerState.Normal:
                    switch (c)
                    {
                        case '\'':
                            Next(); // Consume the quote
                            state = TokenizerState.SingleQuote;
                            break;
                        case '"':
                            Next(); // Consume the quote
                            state = TokenizerState.DoubleQuote;
                            break;
                        case '\\':
                            Next(); // Consume the backslash
                            state = TokenizerState.Escape;
                            break;
                        case '$' when tokenBuilder.Length > 0:
                            // Return the current token before starting a variable
                            return new Token
                            {
                                Type = tokenType,
                                Value = tokenBuilder.ToString()
                            };
                        case '$':
                        {
                            Next(); // Consume the $
                            tokenType = TokenType.Variable;

                            if (Peek() == '{')
                            {
                                Next(); // Consume the {
                                state = TokenizerState.VariableBrace;
                            }
                            else
                            {
                                state = TokenizerState.Variable;
                            }

                            break;
                        }
                        default:
                        {
                            if (ShellOperators.IsOperator(c.ToString()))
                            {
                                if (tokenBuilder.Length > 0)
                                {
                                    // Return the current token before starting an operator
                                    return new Token
                                    {
                                        Type = tokenType,
                                        Value = tokenBuilder.ToString()
                                    };
                                }

                                state = TokenizerState.Operator;
                                tokenType = TokenType.Operator;
                            }
                            else if (IsWhitespace(c))
                            {
                                // End of token
                                if (tokenBuilder.Length > 0)
                                {
                                    Next(); // Consume the whitespace
                                    return new Token
                                    {
                                        Type = tokenType,
                                        Value = tokenBuilder.ToString()
                                    };
                                }

                                Next(); // Skip whitespace
                            }
                            else if (IsSpecialNumber(c) && ShellOperators.All.Contains(ReadNext().ToString()))
                            {
                                _specialNumber = c;
                                Next();
                            }
                            else
                            {
                                tokenBuilder.Append(Next());
                            }

                            break;
                        }
                    }

                    break;

                case TokenizerState.SingleQuote:
                    if (c == '\'')
                    {
                        Next(); // Consume the closing quote
                        state = TokenizerState.Normal;
                    }
                    else
                    {
                        tokenBuilder.Append(Next());
                    }

                    break;

                case TokenizerState.DoubleQuote:
                    switch (c)
                    {
                        case '"':
                            Next(); // Consume the closing quote
                            state = TokenizerState.Normal;
                            break;
                        case '\\':
                            Next(); // Consume the backslash
                            state = TokenizerState.EscapeInDouble;
                            break;
                        case '$':
                            // Variable expansion in double quotes - for simplicity, treating as regular text here
                            tokenBuilder.Append(Next());
                            break;
                        default:
                            tokenBuilder.Append(Next());
                            break;
                    }

                    break;

                case TokenizerState.Escape:
                    // In normal mode, backslash escapes any character
                    tokenBuilder.Append(Next());
                    state = TokenizerState.Normal;
                    break;

                case TokenizerState.EscapeInDouble:
                    // In double quotes, backslash only escapes $, `, ", \ and newline
                    if (!_escapeInDoubleChars.Contains(Peek()))
                    {
                        tokenBuilder.Append('\\');
                        state = TokenizerState.DoubleQuote;
                        break;
                    }
                    
                    tokenBuilder.Append(Next());
                    state = TokenizerState.DoubleQuote;
                    break;

                case TokenizerState.Variable:
                    // Variable names consist of alphanumeric chars and _
                    if (char.IsLetterOrDigit(c) || c == '_')
                    {
                        tokenBuilder.Append(Next());
                    }
                    else
                    {
                        // End of variable name
                        return new Token
                        {
                            Type = tokenType,
                            Value = tokenBuilder.ToString()
                        };
                    }

                    break;

                case TokenizerState.VariableBrace:
                    if (c == '}')
                    {
                        Next(); // Consume the closing brace
                        return new Token
                        {
                            Type = tokenType,
                            Value = tokenBuilder.ToString()
                        };
                    }

                    tokenBuilder.Append(Next());

                    break;

                case TokenizerState.Operator:
                    string currentOp;
                    if(_specialNumber == null) currentOp = tokenBuilder.ToString() + c;
                    else
                    {
                        currentOp = _specialNumber +  tokenBuilder.ToString() + c;
                    }

                    // Check if adding this character would still form a valid operator
                    var foundLongerOp = ShellOperators.All.Any(op => op.StartsWith(currentOp));

                    if (foundLongerOp)
                    {
                        tokenBuilder.Append(_specialNumber);
                        _specialNumber = null;
                        // Continue building the operator
                        tokenBuilder.Append(Next());
                    }
                    else
                    {
                        // Return the current operator
                        return new Token
                        {
                            Type = tokenType,
                            Value = tokenBuilder.ToString()
                        };
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return new Token
        {
            Type = tokenType,
            Value = tokenBuilder.ToString()
        };
    }
    
    /// <summary>
    /// Tokenizes the input string and returns all tokens.
    /// </summary>
    public List<Token> TokenizeAll()
    {
        var tokens = new List<Token>();
        Token token;
        while ((token = NextToken()).Type != TokenType.Eof)
        {
            tokens.Add(token);
        }

        return tokens;
    }
}