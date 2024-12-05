using System.Text.Json.Serialization;

namespace PortlandWeather.DTO;

public class Coord
{
    [JsonPropertyName("lat")]
    public required double Latitude { get; set; }

    [JsonPropertyName("lon")]
    public required double Longitude { get; set; }
}
