namespace src;

public static class PathVariable
{
    public static string Check(string token)
    {
        var pathVar = Environment.GetEnvironmentVariable("PATH");

        if (pathVar == null) return string.Empty;

        var paths = pathVar.Split(Path.PathSeparator);

        foreach (var path in paths)
        {
            var command = ScanDirectory(path, token);

            if (!string.IsNullOrEmpty(command)) return command;
        }

        return string.Empty;
    }

    private static string ScanDirectory(string path, string token)
    {
        if (!Directory.Exists(path)) return string.Empty;
        
        var files = Directory.GetFiles(path);

        foreach (var f in files)
        {
            var fileName = Path.GetFileName(f);
            
            if(fileName == token) return f;
        }

        return string.Empty;
    }
}