namespace TemperatureAnalysis
{
    public static class BatchProcessor
    {
        public static void ProcessBatch(string filename)
        {
            try
            {
                if (!FileUtils.TryReadAllLines(filename, out string[] lines))
                {
                    Console.WriteLine("Error: File not found.");
                    return;
                }
                var records = new List<TemperatureRecord>();
                var badLines = new List<(int index, string line, string error)>();

                for (int i = 0; i < lines.Length; i++)
                {
                    var (record, error) = TemperatureParser.Parse(lines[i].Trim());
                    if (record != null)
                        records.Add(record);
                    else if (error != null)
                        badLines.Add((i, lines[i], error));
                }

                if (records.Count == 0)
                {
                    Console.WriteLine("No valid temperature data found.");
                    return;
                }

                var stats = new TemperatureStatistics(records);

                TemperatureReport.PrintSummary(
                    lines.Length, records.Count, badLines.Count, stats, badLines);

                string outName = filename + "_summary.txt";
                try
                {
                    TemperatureReport.SaveSummary(
                        outName, filename, lines.Length, records.Count, badLines.Count, stats, badLines);
                    Console.WriteLine($"Report saved to {outName}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error saving file: {e.Message}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error processing file: {e.Message}");
            }
        }
    }
}
