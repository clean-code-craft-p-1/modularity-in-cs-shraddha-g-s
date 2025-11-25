using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemperatureAnalysis
{
    public static class FileCleaner
    {
        public static void Clean(params string[] files)
        {
            foreach (var file in files)
            {
                try
                {
                    if (File.Exists(file))
                        File.Delete(file);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Warning: Could not clean up file {file}: {e.Message}");
                }
            }
        }
    }
}


