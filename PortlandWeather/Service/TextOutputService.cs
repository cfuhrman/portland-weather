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

        Console.WriteLine(
            "Current Temperature : {0:0.0}° (feels like {1:0.0}°)",
            ConvertToFahrenheit(currentWeather.Main.Temperature),
            ConvertToFahrenheit(currentWeather.Main.FeelsLike)
            );

        Console.WriteLine(
            "Temperature Range   : {0:0.0}° (Low) to {1:0.0}° (High)\n",
            ConvertToFahrenheit(currentWeather.Main.MinTemperature),
            ConvertToFahrenheit(currentWeather.Main.MaxTemperature)
            );

        Console.Write(
            "Conditions are {0} with {1}, a humidity of {2}%,\n wind speed of {3:0.00} mph, ",
            currentWeather.Weather[0].Main.ToLower(),
            currentWeather.Weather[0].Description,
            currentWeather.Main.Humidity,
            ConvertToMilesPerHour(currentWeather.Wind.Speed)
            );

        if (currentWeather.Wind.Gust != null)
        {
            Console.WriteLine("and gusts up to {0:0.00} mph.\n", ConvertToMilesPerHour((double)currentWeather.Wind.Gust));
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
    public static double ConvertToFahrenheit(double kelvin)
    {
        return ((kelvin - 273.15) * 1.8) + 32;
    }

    /// <summary>
    /// Converts meters per second to miles per hour
    /// </summary>
    /// <param name="msec">Speed in meters/sec</param>
    /// <returns>Speed in miles/hour</returns>
    public static double ConvertToMilesPerHour(double msec)
    {
        return msec * METER_PER_SEC_TO_MPH;
    }
}
