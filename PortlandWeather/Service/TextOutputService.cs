using System.Text;
using PortlandWeather.DTO;

namespace PortlandWeather.Service;

public class TextOutputService
{
    /// <summary>
    /// 1 Meter Per Second to MPH
    /// </summary>
    const double METER_PER_SEC_TO_MPH = 2.2369363;

    /// <summary>
    /// Icon ID to emoji Map
    /// </summary>
    private Dictionary<string, string> iconMap = new Dictionary<string, string> {
        { "01d", "🌣"},
        { "02d", "🌤"},
        { "03d", "🌥"},
        { "04d", "🌥"},
        { "09d", "🌧"},
        { "10d", "🌧"},
        { "11d", "🌩"},
        { "13d", "🌨"},
        { "50d", "🌫"},
        };

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
            "Current Temperature : {0}° (feels like {1}°)\n",
            Math.Round(ConvertToFahrenheit(currentWeather.Main.Temperature), MidpointRounding.AwayFromZero),
            Math.Round(ConvertToFahrenheit(currentWeather.Main.FeelsLike), MidpointRounding.AwayFromZero)
        );

        sb.AppendFormat(
            "Temperature Range   : {0}° (Low) to {1}° (High)\n\n",
            Math.Round(ConvertToFahrenheit(currentWeather.Main.MinTemperature), MidpointRounding.AwayFromZero),
            Math.Round(ConvertToFahrenheit(currentWeather.Main.MaxTemperature), MidpointRounding.AwayFromZero)
        );

        sb.AppendFormat(
            "Conditions are {0} {1} with {2}, a humidity of {3}%,\n wind speed of {4} mph, ",
            currentWeather.Weather[0].Main.ToLower(),
            iconMap[currentWeather.Weather[0].Icon],
            currentWeather.Weather[0].Description,
            currentWeather.Main.Humidity,
            Math.Round(ConvertToMilesPerHour(currentWeather.Wind.Speed), MidpointRounding.AwayFromZero)
        );

        if (currentWeather.Clouds.All != 0)
            sb.AppendFormat(
                "{0}% cloud cover, ",
                currentWeather.Clouds.All
            );

        if (currentWeather.Wind.Gust != null)
        {
            sb.AppendFormat("and gusts up to {0} mph.\n\n", Math.Round(ConvertToMilesPerHour((double)currentWeather.Wind.Gust), MidpointRounding.AwayFromZero));
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
