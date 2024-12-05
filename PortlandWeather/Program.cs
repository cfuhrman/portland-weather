using PortlandWeather.DTO;
using PortlandWeather.Service;
using System.Text.Json;

namespace PortlandWeather;

internal class Program
{
    const double Latitude = 45.5148142;
    const double Longitude = -122.6816836;
    const double OneMeterPerSecInMPH = 2.2369363;

    static void Main(string[] args)
    {
        var client = new HttpClient();

        client.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/");
        client.DefaultRequestHeaders.UserAgent.ParseAdd("dotNet Weather Service");

        string key = Environment.GetEnvironmentVariable("KEY") ?? string.Empty;

        WeatherService service = new WeatherService(client, key);

        var weather = service.GetCurrentWeatherAsync(Latitude, Longitude).Result;
        var currentTime = DateTimeOffset.FromUnixTimeSeconds(weather.UNIXTimeStamp).ToLocalTime();

        // For debugging
        // Console.WriteLine(JsonSerializer.Serialize<CurrentWeather>(weather, new JsonSerializerOptions {  WriteIndented = true }));

        Console.WriteLine("** Conditions in {0} as of {1} **\n", weather.Name, currentTime.ToString("f"));

        Console.WriteLine($"Current Temperature : {ConvertToFahrenheit(weather.Main.Temperature)}° (feels like {ConvertToFahrenheit(weather.Main.FeelsLike)}°)");
        Console.WriteLine($"Temperature Range   : {ConvertToFahrenheit(weather.Main.MinTemperature)}° (Low) to {ConvertToFahrenheit(weather.Main.MaxTemperature)}° (High)\n");

        Console.Write($"Conditions are {weather.Weather[0].Main.ToLower()} with {weather.Weather[0].Description}, a humidity of {weather.Main.Humidity}%,\n wind speed of {ConvertToMilesPerHour(weather.Wind.Speed)} mph, ");

        if (weather.Wind.Gust != null)
        {
            Console.WriteLine($"and gusts up to {ConvertToMilesPerHour((double) weather.Wind.Gust)} mph.\n");
        }
        else
        {
            Console.WriteLine($"and no gusts.\n");
        }

        var sunrise = DateTimeOffset.FromUnixTimeSeconds(weather.Sys.Sunrise).ToLocalTime();
        var sunset  = DateTimeOffset.FromUnixTimeSeconds(weather.Sys.Sunset).ToLocalTime();

        Console.WriteLine("The sun will rise at {0} and will set at {1}\n", sunrise.ToString("t"), sunset.ToString("t"));


        Console.WriteLine("Data by OpenWeatherMap");
    }

    /// <summary>
    /// Converts Kelvin to Fahrenheit
    ///
    /// Note : Will be rounded to two decimal places
    /// </summary>
    /// <param name="kelvin">Temperature in Kelvin</param>
    /// <returns>Temperature in Fahrenheit</returns>
    static double ConvertToFahrenheit(double kelvin)
    {
        double fahrenheit = ((kelvin - 273.15) * 1.8) + 32;

        return Math.Round(fahrenheit, 2);
    }

    /// <summary>
    /// Converts meters per second to miles per hour
    /// </summary>
    /// <param name="msec">Speed in meters/sec</param>
    /// <returns>Speed in miles/hour</returns>
    static double ConvertToMilesPerHour(double msec)
    {
        double mph = msec * OneMeterPerSecInMPH;

        return Math.Round(mph, 2);
    }
}
