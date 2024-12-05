using PortlandWeather.DTO;
using System.Net.Http.Json;
using System.Text.Json;

namespace PortlandWeather.Service;

public sealed class WeatherService : IDisposable
{
    private HttpClient _httpClient;
    private string _apiKey;

    public WeatherService(HttpClient httpClient, string key)
    {
        _httpClient = httpClient;
        _apiKey = key;
    }

    public async Task<CurrentWeather> GetCurrentWeatherAsync(double latitude, double longitude)
    {
        // weather?lat=44.34&lon=10.99&appid={API key}
        string parameters = $"weather?lat={latitude.ToString()}&lon={longitude.ToString()}&appid={_apiKey}";

        CurrentWeather? weather = await _httpClient.GetFromJsonAsync<CurrentWeather>(parameters, new JsonSerializerOptions(JsonSerializerDefaults.Web)) ?? null;

        return weather;
    }

    public void Dispose() => _httpClient?.Dispose();
}
