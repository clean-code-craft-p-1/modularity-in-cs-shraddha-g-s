using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemperatureAnalysis
{
    public static class SummaryVerifier
    {
        public static void Verify(string summaryFile)
        {
            if (File.Exists(summaryFile))
            {
                Console.WriteLine($"\nSummary file created: {summaryFile}");
                string content = File.ReadAllText(summaryFile);

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
        }
    }
}
