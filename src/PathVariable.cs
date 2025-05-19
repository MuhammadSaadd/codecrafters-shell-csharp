namespace src;

public static class PathVariable
{
    public static bool TryGet(string token, out string? value)
    {
        var pathVar = Environment.GetEnvironmentVariable("PATH");

        if (pathVar == null)
        {
            value = null;
            return false;
        }

        var paths = pathVar.Split(Path.PathSeparator);

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