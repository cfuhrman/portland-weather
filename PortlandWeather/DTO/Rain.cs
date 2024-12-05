using System.Text.Json.Serialization;

namespace PortlandWeather.DTO;

public class Rain
{
    [JsonPropertyName("1h")]
    public required float OneHour { get; set; }
}
