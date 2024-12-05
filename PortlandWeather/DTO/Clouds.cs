using System.Text.Json.Serialization;

namespace PortlandWeather.DTO;

public class Clouds
{
    [JsonPropertyName("all")]
    public required int All {  get; set; }
}
