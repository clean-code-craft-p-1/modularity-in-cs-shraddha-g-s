namespace TemperatureAnalysis
{
    public static class FileUtils
    {
        public static bool TryReadAllLines(string path, out string[] lines)
        {
            lines = Array.Empty<string>();
            if (!File.Exists(path))
                return false;
            lines = File.ReadAllLines(path);
            return true;
        }

        public static bool TryWriteAllText(string path, string content)
        {
            try
            {
                File.WriteAllText(path, content);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool TryDelete(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                    return true;
                }
            }
            catch { }
            return false;
        }
    }
}

