using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemperatureAnalysis
{
    public static class TestDataGenerator
    {
        public static void Generate(string filename, string[] data)
        {
            string content = string.Join(Environment.NewLine, data);
            if (FileUtils.TryWriteAllText(filename, content))
            {
                Console.WriteLine($"Created test file: {filename}");
            }
            else
            {
                Console.WriteLine($"Failed to create test file: {filename}");
            }
        }
    }
}

