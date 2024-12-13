using PortlandWeather.DTO;

namespace PortlandWeather.Service;

public class TextOutputService
{
    /// <summary>
    /// 1 Meter Per Second to MPH
    /// </summary>
    const double METER_PER_SEC_TO_MPH = 2.2369363;

    public required CurrentWeather currentWeather { get; set; }

    /// <summary>
    /// Generates output to console
    /// </summary>
    public void Render()
    {
        var currentTime = DateTimeOffset.FromUnixTimeSeconds(currentWeather.UNIXTimeStamp).ToLocalTime();
        var sunrise = DateTimeOffset.FromUnixTimeSeconds(currentWeather.Sys.Sunrise).ToLocalTime();
        var sunset = DateTimeOffset.FromUnixTimeSeconds(currentWeather.Sys.Sunset).ToLocalTime();

        Console.WriteLine("** Conditions in {0} as of {1} **\n", currentWeather.Name, currentTime.ToString("f"));

        Console.WriteLine($"Current Temperature : {ConvertToFahrenheit(currentWeather.Main.Temperature)}° (feels like {ConvertToFahrenheit(currentWeather.Main.FeelsLike)}°)");
        Console.WriteLine($"Temperature Range   : {ConvertToFahrenheit(currentWeather.Main.MinTemperature)}° (Low) to {ConvertToFahrenheit(currentWeather.Main.MaxTemperature)}° (High)\n");

        Console.Write($"Conditions are {currentWeather.Weather[0].Main.ToLower()} with {currentWeather.Weather[0].Description}, a humidity of {currentWeather.Main.Humidity}%,\n wind speed of {ConvertToMilesPerHour(currentWeather.Wind.Speed)} mph, ");

        if (currentWeather.Wind.Gust != null)
        {
            Console.WriteLine($"and gusts up to {ConvertToMilesPerHour((double)currentWeather.Wind.Gust)} mph.\n");
        }
        else
        {
            Console.WriteLine($"and no gusts.\n");
        }

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
        double mph = msec * METER_PER_SEC_TO_MPH;

        return Math.Round(mph, 2);
    }
}
