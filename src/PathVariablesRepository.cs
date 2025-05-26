namespace src;

public static class PathVariablesRepository
{
    private static string? PathVar => Environment.GetEnvironmentVariable("PATH");
    
    public static bool TryGet(string token, out string? value)
    {
        if (PathVar == null)
        {
            value = null;
            return false;
        }

        var paths = PathVar.Split(Path.PathSeparator);

        foreach (var path in paths)
        {
            var command = ScanDirectory(path, token);

            if (string.IsNullOrEmpty(command)) continue;
            
            value = command;
            return true;
        }

        value = null;
        return false;
    }

    public static List<string> GetAllCommands()
    {
        if (PathVar == null) return [];
        var paths = PathVar.Split(Path.PathSeparator);

        List<string> commands = [];
     
        foreach (var path in paths)
        {
            if (!Directory.Exists(path)) continue;
            
            string?[] files = Directory.GetFiles(path);
            
            files.ToList().ForEach(file => commands.Add(Path.GetFileName(file)!));
        }
        
        return commands;
    }

    private static string? ScanDirectory(string path, string token)
    {
        if (!Directory.Exists(path)) return string.Empty;
        
        string?[] files = Directory.GetFiles(path);

        foreach (var f in files)
        {
            var fileName = Path.GetFileName(f);
            
            if(fileName == token) return f;
        }

        return string.Empty;
    }
}