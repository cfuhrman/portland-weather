using System.Text.Json.Serialization;

namespace PortlandWeather.DTO;

public class CurrentWeather
{
    [JsonPropertyName("coord")]
    public required Coord Coordinates { get; set; }

    [JsonPropertyName("weather")]
    public required List<Weather> Weather { get; set; }

    [JsonPropertyName("base")]
    public required string Base {  get; set; }

    [JsonPropertyName("main")]
    public required Main Main { get; set; }

    [JsonPropertyName("visibility")]
    public required int Visibility { get; set; }

    [JsonPropertyName("wind")]
    public required Wind Wind { get; set; }

    [JsonPropertyName("rain")]
    public Rain? Rain { get; set; }

    [JsonPropertyName("clouds")]
    public required Clouds Clouds { get; set; }

    [JsonPropertyName("dt")]
    public required long UNIXTimeStamp { get; set; }

    [JsonPropertyName("sys")]
    public required Sys Sys { get; set; }

    [JsonPropertyName("timezone")]
    public required int TimeZone { get; set; }

    [JsonPropertyName("id")]
    public required int Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("cod")]
    public required int Cod { get; set; }
}
