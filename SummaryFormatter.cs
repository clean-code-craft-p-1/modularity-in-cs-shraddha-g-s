using System.Text;

namespace TemperatureAnalysis
{
    public static class SummaryFormatter
    {
        public static string Format(
            string? filename,
            int total, int valid, int errors,
            TemperatureStatistics? stats,
            List<(int index, string line, string error)> badLines,
            bool forConsole){
            var summary = new StringBuilder();
            if (forConsole) {
                summary.AppendLine(new string('=', 60));
                summary.AppendLine("Temperature Analysis Summary");
                summary.AppendLine(new string('=', 60));
            }
            else {
                summary.AppendLine("Temperature Analysis Summary");
                summary.AppendLine(new string('=', 50));
                if (!string.IsNullOrEmpty(filename))
                    summary.AppendLine($"File analyzed: {filename}");
            }

            summary.AppendLine($"Total readings: {total}");
            summary.AppendLine($"Valid readings: {valid}");
            summary.AppendLine($"Errors: {errors}");

            if (stats != null)
            {
                summary.AppendLine($"Max temperature: {stats.Max:F2}");
                summary.AppendLine($"Min temperature: {stats.Min:F2}");
                summary.AppendLine($"Average temperature: {stats.Average:F2}");
            }

            summary.AppendLine(new string('-', 60));

            if (errors > 0)
            {
                summary.AppendLine(forConsole ? "Invalid lines:" : "\nInvalid lines:");
                foreach (var (idx, bad, err) in badLines)
                    summary.AppendLine($"  Line {idx + 1}: {bad} ({err})");
            }

            return summary.ToString();
        }
    }
}