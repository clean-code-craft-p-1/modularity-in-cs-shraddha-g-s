using System.Globalization;

namespace TemperatureAnalysis
{
    public class TemperatureParser
    {
        public static (TemperatureRecord? record, string? error) Parse(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
                return (null, null);

            string[] parts = line.Split(',');
            if (parts.Length != 2)
                return (null, "Invalid format");

            string timestamp = parts[0].Trim();
            string valueStr = parts[1].Trim();

            if (timestamp.Split(':').Length != 3)
                return (null, "Invalid timestamp format");

            if (!double.TryParse(valueStr, NumberStyles.Float, CultureInfo.InvariantCulture, out double temp))
                return (null, "Invalid temperature value");

            if (temp < -100 || temp > 200)
                return (null, "Temperature out of range");

            return (new TemperatureRecord(timestamp, temp), null);
        }
    }
}