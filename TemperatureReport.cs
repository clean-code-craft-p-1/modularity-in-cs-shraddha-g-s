namespace TemperatureAnalysis
{
    public class TemperatureReport
    {
        public static void OutputSummary(
            int total, int valid, int errors,
            TemperatureStatistics? stats,
            List<(int index, string line, string error)> badLines,
            bool toConsole,
            string? outName = null,
            string? filename = null)
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
