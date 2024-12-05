using System.Text.Json.Serialization;

namespace PortlandWeather.DTO;

public class Sys
{
    [JsonPropertyName("type")]
    public required int Type { get; set; }

    [JsonPropertyName("id")]
    public required int Id { get; set; }

    [JsonPropertyName("country")]
    public required string Country { get; set; }

    [JsonPropertyName("sunrise")]
    public required long Sunrise { get; set; }

    [JsonPropertyName("sunset")]
    public required long Sunset { get; set; }
}
