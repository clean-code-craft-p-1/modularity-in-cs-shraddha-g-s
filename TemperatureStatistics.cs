namespace TemperatureAnalysis
{
    public class TemperatureStatistics
    {
        public double Max { get; }
        public double Min { get; }
        public double Average { get; }

        public TemperatureStatistics(IEnumerable<TemperatureRecord> records)
        {
            var temps = records.Select(r => r.Temperature).ToList();
            Max = temps.Max();
            Min = temps.Min();
            Average = temps.Average();
        }
    }
}