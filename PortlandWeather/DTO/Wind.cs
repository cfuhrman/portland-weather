using System.Text.Json.Serialization;

namespace PortlandWeather.DTO;

public class Wind
{
    [JsonPropertyName("speed")]
    public required double Speed {  get; set; }

    [JsonPropertyName("deg")]
    public required int Degree { get; set; }

    [JsonPropertyName("gust")]
    public double? Gust { get; set; }
}
