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
            File.WriteAllLines(filename, data);
            Console.WriteLine($"Created test file: {filename}");
        }
    }
}
