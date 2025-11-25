using System.Collections.Generic;

namespace TemperatureAnalysis
{
    public class TemperatureReport
    {
        public static void PrintSummary(
            int total, int valid, int errors,
            TemperatureStatistics? stats,
            List<(int index, string line, string error)> badLines)
        {
            string summary = SummaryFormatter.Format(
                null, total, valid, errors, stats, badLines, true);
            System.Console.WriteLine(summary);
        }

        public static void SaveSummary(
            string outName, string filename,
            int total, int valid, int errors,
            TemperatureStatistics? stats,
            List<(int index, string line, string error)> badLines)
        {
            string summary = SummaryFormatter.Format(
                filename, total, valid, errors, stats, badLines, false);
            FileUtils.TryWriteAllText(outName, summary);
        }
    }
}