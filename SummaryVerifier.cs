namespace TemperatureAnalysis
{
    public static class SummaryVerifier
    {
        public static void Verify(string summaryFile)
        {
            if (FileUtils.TryReadAllLines(summaryFile, out string[] content))
            {
                Console.WriteLine($"\nSummary file created: {summaryFile}");

                if (content.Contains("Total readings: 10") &&
                    content.Contains("Valid readings: 10") &&
                    content.Contains("Errors: 0"))
                {
                    Console.WriteLine("✓ Summary file contents verified");
                }
                else
                {
                    Console.WriteLine("✗ Summary file verification failed");
                }
            }
            else
            {
                Console.WriteLine($"Summary file not found: {summaryFile}");
            }
        }
    }
}

