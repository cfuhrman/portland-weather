using System.Text.Json.Serialization;

namespace PortlandWeather.DTO;

public class Main
{
    [JsonPropertyName("temp")]
    public required float Temperature { get; set; }

    [JsonPropertyName("feels_like")]
    public required float FeelsLike { get; set; }

    [JsonPropertyName("temp_min")]
    public required float MinTemperature { get; set; }

    [JsonPropertyName("temp_max")]
    public required float MaxTemperature { get; set; }

    [JsonPropertyName("pressure")]
    public required int Pressure { get; set; }

    [JsonPropertyName("humidity")]
    public required int Humidity { get; set; }

    [JsonPropertyName("sea_level")]
    public required int SeaLevel { get; set; }

    [JsonPropertyName("grnd_level")]
    public required int GroundLevel { get; set; }
}
