namespace TemperatureAnalysis
{
    public class TemperatureRecord
    {
        public string Timestamp { get; }
        public double Temperature { get; }

        public TemperatureRecord(string timestamp, double temperature)
        {
            Timestamp = timestamp;
            Temperature = temperature;
        }
    }
}