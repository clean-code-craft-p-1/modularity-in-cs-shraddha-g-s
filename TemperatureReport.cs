namespace TemperatureAnalysis
{
    public class TemperatureReport
    {
        public static void PrintSummary(
            int total, int valid, int errors,
            TemperatureStatistics? stats,
            List<(int index, string line, string error)> badLines)
        {
            Console.WriteLine(new string('=', 60));
            Console.WriteLine("Temperature Analysis Summary");
            Console.WriteLine(new string('=', 60));
            Console.WriteLine($"Total readings: {total}");
            Console.WriteLine($"Valid readings: {valid}");
            Console.WriteLine($"Errors: {errors}");
            Console.WriteLine(new string('-', 60));
            if (stats != null)
            {
                Console.WriteLine($"Max temperature: {stats.Max:F2}");
                Console.WriteLine($"Min temperature: {stats.Min:F2}");
                Console.WriteLine($"Average temperature: {stats.Average:F2}");
            }
            Console.WriteLine(new string('-', 60));
            if (errors > 0)
            {
                Console.WriteLine("Invalid lines:");
                foreach (var (idx, bad, err) in badLines)
                    Console.WriteLine($"  Line {idx + 1}: {bad} ({err})");
            }
        }

        public static void SaveSummary(
            string outName, string filename,
            int total, int valid, int errors,
            TemperatureStatistics? stats,
            List<(int index, string line, string error)> badLines)
        {
            using (var writer = new StreamWriter(outName))
            {
                writer.WriteLine("Temperature Analysis Summary");
                writer.WriteLine(new string('=', 50));
                writer.WriteLine($"File analyzed: {filename}");
                writer.WriteLine($"Total readings: {total}");
                writer.WriteLine($"Valid readings: {valid}");
                writer.WriteLine($"Errors: {errors}");
                if (stats != null)
                {
                    writer.WriteLine($"Max temperature: {stats.Max:F2}");
                    writer.WriteLine($"Min temperature: {stats.Min:F2}");
                    writer.WriteLine($"Average temperature: {stats.Average:F2}");
                }
                writer.WriteLine(new string('-', 60));
                if (errors > 0)
                {
                    writer.WriteLine("\nInvalid lines:");
                    foreach (var (idx, bad, err) in badLines)
                        writer.WriteLine($"  Line {idx + 1}: {bad} ({err})");
                }
            }
        }
    }
}