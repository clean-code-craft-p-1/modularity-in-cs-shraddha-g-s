using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace TemperatureAnalysis
{
    class Program
    {
        static void ProcessBatch(string filename)
        {
            try
            {
                if (!File.Exists(filename))
                {
                    Console.WriteLine("Error: File not found.");
                    return;
                }

                string[] lines = File.ReadAllLines(filename);

                var temps = new List<double>();
                var timestamps = new List<string>();
                int errors = 0;
                var badLines = new List<(int index, string line)>();

                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i].Trim();
                    if (string.IsNullOrEmpty(line))
                        continue;

                    string[] parts = line.Split(',');
                    if (parts.Length != 2)
                    {
                        errors++;
                        badLines.Add((i, line));
                        continue;
                    }

                    string timestamp = parts[0].Trim();
                    string valueStr = parts[1].Trim();

                    // Validate timestamp (expecting HH:MM:SS format)
                    if (timestamp.Split(':').Length != 3)
                    {
                        errors++;
                        badLines.Add((i, line));
                        continue;
                    }

                    // Try to parse temperature value
                    if (!double.TryParse(valueStr, NumberStyles.Float, CultureInfo.InvariantCulture, out double temp))
                    {
                        errors++;
                        badLines.Add((i, line));
                        continue;
                    }

                    // Drop impossible temperatures
                    if (temp < -100 || temp > 200)
                    {
                        errors++;
                        badLines.Add((i, line));
                        continue;
                    }

                    temps.Add(temp);
                    timestamps.Add(timestamp);
                }

                if (temps.Count == 0)
                {
                    Console.WriteLine("No valid temperature data found.");
                    return;
                }

                // Calculate statistics
                double maxTemp = temps.Max();
                double minTemp = temps.Min();
                double avgTemp = temps.Average();

                // Print summary
                Console.WriteLine(new string('=', 60));
                Console.WriteLine("Temperature Analysis Summary");
                Console.WriteLine(new string('=', 60));
                Console.WriteLine($"Total readings: {lines.Length}");
                Console.WriteLine($"Valid readings: {temps.Count}");
                Console.WriteLine($"Errors: {errors}");
                Console.WriteLine(new string('-', 60));
                Console.WriteLine($"Max temperature: {maxTemp:F2}");
                Console.WriteLine($"Min temperature: {minTemp:F2}");
                Console.WriteLine($"Average temperature: {avgTemp:F2}");
                Console.WriteLine(new string('-', 60));

                // Print invalid lines (verbose)
                if (errors > 0)
                {
                    Console.WriteLine("Invalid lines:");
                    foreach (var (idx, bad) in badLines)
                    {
                        Console.WriteLine($"  Line {idx + 1}: {bad}");
                    }
                }

                // Save report
                string outName = filename + "_summary.txt";
                try
                {
                    using (var writer = new StreamWriter(outName))
                    {
                        writer.WriteLine("Temperature Analysis Summary");
                        writer.WriteLine(new string('=', 50));
                        writer.WriteLine($"File analyzed: {filename}");
                        writer.WriteLine($"Total readings: {lines.Length}");
                        writer.WriteLine($"Valid readings: {temps.Count}");
                        writer.WriteLine($"Errors: {errors}");
                        writer.WriteLine($"Max temperature: {maxTemp:F2}");
                        writer.WriteLine($"Min temperature: {minTemp:F2}");
                        writer.WriteLine($"Average temperature: {avgTemp:F2}");
                        writer.WriteLine(new string('-', 60));
                        
                        if (errors > 0)
                        {
                            writer.WriteLine("\nInvalid lines:");
                            foreach (var (idx, bad) in badLines)
                            {
                                writer.WriteLine($"  Line {idx + 1}: {bad}");
                            }
                        }
                    }
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

        static void Main(string[] args)
        {
            // Generate test data file
            string testFilename = "test_temps.csv";
            string[] testData = {
                "09:15:30,23.5",
                "09:16:00,24.1",
                "09:16:30,22.8",
                "09:17:00,25.3",
                "09:17:30,23.9",
                "09:18:00,24.7",
                "09:18:30,22.4",
                "09:19:00,26.1",
                "09:19:30,23.2",
                "09:20:00,25.0"
            };

            File.WriteAllLines(testFilename, testData);
            Console.WriteLine($"Created test file: {testFilename}");

            // Process the test file
            ProcessBatch(testFilename);

            // Verify the summary file was created
            string summaryFile = testFilename + "_summary.txt";
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

            // Clean up test files
            try
            {
                if (File.Exists(testFilename))
                    File.Delete(testFilename);
                if (File.Exists(summaryFile))
                    File.Delete(summaryFile);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Warning: Could not clean up files: {e.Message}");
            }
        }
    }
}