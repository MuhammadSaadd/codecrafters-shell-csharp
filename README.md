In this project, I built my own shell by following the Codecrafters challenge instructions and developed it using my preferred language, C#.

Supported Features:

    All common built-in commands such as echo, pwd, exit, and type.

    External commands added to the environment variables.

    Navigation between directories using the cd command.

    Most common quoting conventions, such as single and double quotes, with support for escape characters and executing quoted executables.

    Redirection of standard output and standard error, with support for appending.

    Auto-completion for built-in commands, external commands, multiple completions, and partial completions.

Components:

    Custom input reader that supports key strokes such as Tab, Backspace, and Enter.

    Tokenizer using a state machine algorithm.

    Token parser.

    Commander that executes both built-in and external commands.

Future Features:

    Pipelines

    Command history