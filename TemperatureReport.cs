namespace TemperatureAnalysis
{
    public class TemperatureReport
    {
        public static void PrintSummary(
            int total, int valid, int errors,
            TemperatureStatistics? stats,
            List<(int index, string line, string error)> badLines)
        {
            OutputSummary(
                null, total, valid, errors, stats, badLines, true, null);
        }

        public static void SaveSummary(
            string outName, string filename,
            int total, int valid, int errors,
            TemperatureStatistics? stats,
            List<(int index, string line, string error)> badLines)
        {
            OutputSummary(
                filename, total, valid, errors, stats, badLines, false, outName);
        }

        private static void OutputSummary(
            string? filename, int total, int valid, int errors,
            TemperatureStatistics? stats,
            List<(int index, string line, string error)> badLines,
            bool toConsole, string? outName)
        {
            string summary = SummaryFormatter.Format(
                filename, total, valid, errors, stats, badLines, toConsole);

            if (toConsole)
                System.Console.WriteLine(summary);
            else if (outName != null)
                FileUtils.TryWriteAllText(outName, summary);
        }
    }
}