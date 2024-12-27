using System.Text;
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
    /// <returns>StringBuilder object</returns>
    public StringBuilder Render()
    {
        var currentTime = DateTimeOffset.FromUnixTimeSeconds(currentWeather.UNIXTimeStamp).ToLocalTime();
        var sunrise = DateTimeOffset.FromUnixTimeSeconds(currentWeather.Sys.Sunrise).ToLocalTime();
        var sunset = DateTimeOffset.FromUnixTimeSeconds(currentWeather.Sys.Sunset).ToLocalTime();

        StringBuilder sb = new StringBuilder();

        sb.AppendFormat(
            "** Conditions in {0} as of {1} **\n\n",
            currentWeather.Name,
            currentTime.ToString("f")
        );

        sb.AppendFormat(
            "Current Temperature : {0:0.0}° (feels like {1:0.0}°)\n",
            ConvertToFahrenheit(currentWeather.Main.Temperature),
            ConvertToFahrenheit(currentWeather.Main.FeelsLike)
        );

        sb.AppendFormat(
            "Temperature Range   : {0:0.0}° (Low) to {1:0.0}° (High)\n\n",
            ConvertToFahrenheit(currentWeather.Main.MinTemperature),
            ConvertToFahrenheit(currentWeather.Main.MaxTemperature)
        );

        sb.AppendFormat(
            "Conditions are {0} with {1}, a humidity of {2}%,\n wind speed of {3:0.00} mph, ",
            currentWeather.Weather[0].Main.ToLower(),
            currentWeather.Weather[0].Description,
            currentWeather.Main.Humidity,
            ConvertToMilesPerHour(currentWeather.Wind.Speed)
        );

        if (currentWeather.Wind.Gust != null)
        {
            sb.AppendFormat("and gusts up to {0:0.00} mph.\n\n", ConvertToMilesPerHour((double)currentWeather.Wind.Gust));
        }
        else
        {
            sb.AppendFormat("and no gusts.\n\n");
        }

        sb.AppendFormat("The sun will rise at {0} and will set at {1}\n\n", sunrise.ToString("t"), sunset.ToString("t"));

        sb.AppendFormat("Data by OpenWeatherMap");

        return sb;
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
