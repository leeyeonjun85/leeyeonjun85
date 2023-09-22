using System.Text.Json;

namespace JsonTask
{
    public class WeatherForecast
    {
        public DateTimeOffset Date { get; set; }
        public int TemperatureCelsius { get; set; }
        public string? Summary { get; set; }
    }


    public class Program
    {
        public static async Task Main()
        {
            var weatherForecast = new WeatherForecast
            {
                Date = DateTime.Parse("2019-08-01"),
                TemperatureCelsius = 25,
                Summary = "Hot"
            };

            string fileName = "WeatherForecast.json";
            using FileStream createStream = File.Create(fileName);
            JsonSerializerOptions options = new () { WriteIndented = true };

            await JsonSerializer.SerializeAsync(createStream, weatherForecast, options);
            await createStream.DisposeAsync();

            Console.WriteLine(File.ReadAllText(fileName));
        }
    }
}