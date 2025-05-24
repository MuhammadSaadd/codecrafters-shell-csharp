using src.Commands;
using src.ShellTokenizer;

namespace src;

public static class Parser
{
    public class ParserInfo
    {
        public string? OutputFile { get; set; } 
        public string? ErrorFile { get; set; }
        public bool AppendOutput { get; set; }
        public bool AppendError { get; set; }
        public List<Token> Tokens { get; set; } = [];
    }

    public static ParserInfo Parse(List<Token> tokens)
    {
        ParserInfo info = new();

        List<Token> cleanedTokens = [];

        try
        {
            for (var i = 0; i < tokens.Count; i++)
            {
                var token = tokens[i].Value;

                if (token == ShellOperators.OutputAppend || token == ShellOperators.StdOutputAppend) // >>  1>>
                {
                    info.OutputFile = tokens[i + 1].Value;
                    info.AppendOutput = true;
                    i++;
                }
                else if (token == ShellOperators.OutputRedirect || token == ShellOperators.StdoutRedirect) // >  1>
                {
                    info.OutputFile = tokens[i + 1].Value;
                    info.AppendOutput = false;
                    i++;
                }
                else if (token == ShellOperators.StderrAppend) //  2>>
                {
                    info.ErrorFile = tokens[i + 1].Value;
                    info.AppendError = true;
                    i++;
                }
                else if (token == ShellOperators.StderrRedirect) // 2>
                {
                    info.ErrorFile = tokens[i + 1].Value;
                    info.AppendError = false;
                    i++;
                }
                else
                {
                    cleanedTokens.Add(tokens[i]);
                }
            }

        }
        catch (Exception)
        {
            throw new ArgumentException("bash: syntax error near unexpected token `newline'");
        }
        
        info.Tokens = cleanedTokens;
        return info;
    }
}