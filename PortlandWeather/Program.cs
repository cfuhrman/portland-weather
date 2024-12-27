using PortlandWeather.DTO;
using PortlandWeather.Service;

namespace PortlandWeather;

internal class Program
{
    /// <summary>
    /// Latitude & Longitude courtesy Google Maps
    /// </summary>
    const double LATITUDE = 45.5148142;
    const double LONGITUDE = -122.6816836;

    const string OPENWEATHERMAP_API_ENDPOINT = "https://api.openweathermap.org/data/2.5/";
    const string USER_AGENT = "dotNet Weather Service";

    /// <summary>
    /// Main entry point
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var client = new HttpClient();

        client.BaseAddress = new Uri(OPENWEATHERMAP_API_ENDPOINT);
        client.DefaultRequestHeaders.UserAgent.ParseAdd(USER_AGENT);

        string key = Environment.GetEnvironmentVariable("WEATHER_KEY") ?? string.Empty;

        // For lat/long, we must use strings to figure out if
        // environmental variables are set or not.
        string latitude = Environment.GetEnvironmentVariable("WEATHER_LATITUDE") ?? LATITUDE.ToString();
        string longitude = Environment.GetEnvironmentVariable("WEATHER_LONGITUDE") ?? LONGITUDE.ToString();

        CurrentWeather weather;
        WeatherService service = new WeatherService(client, key);

        try
        {
            weather = service.GetCurrentWeatherAsync(Convert.ToDouble(latitude), Convert.ToDouble(longitude)).Result;
        }
        catch (AggregateException aex)
        {
            Console.WriteLine($"ERROR: {aex.Message} : {aex.StackTrace}");
            Console.WriteLine("Did you remember to set WEATHER_KEY environment variable?");
            return;
        }

        TextOutputService outputService = new TextOutputService()
        {
            currentWeather = weather
        };

        Console.WriteLine(outputService.Render());
    }
}
