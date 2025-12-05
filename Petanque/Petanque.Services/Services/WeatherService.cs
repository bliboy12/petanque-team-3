using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Petanque.Contracts.Responses;
using Petanque.Services.Interfaces;

namespace Petanque.Services.Services;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private const string OpenMeteoBaseUrl = "https://api.open-meteo.com/v1/forecast";

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherResponseContract> GetWeatherForecastAsync(DateTime date, double latitude, double longitude)
    {
        var dateString = date.ToString("yyyy-MM-dd");
        var url = $"{OpenMeteoBaseUrl}?latitude={latitude}&longitude={longitude}&hourly=temperature_2m,precipitation&start_date={dateString}&end_date={dateString}&timezone=Europe/Brussels";

        var response = await _httpClient.GetFromJsonAsync<OpenMeteoResponse>(url);

        if (response == null || response.Hourly == null || response.Hourly.Time == null || response.Hourly.Time.Count == 0)
        {
            throw new Exception("Failed to fetch weather data from Open-Meteo API");
        }

        // Find the index for 12:00 (noon)
        var noonIndex = -1;
        for (int i = 0; i < response.Hourly.Time.Count; i++)
        {
            var timeStr = response.Hourly.Time[i];
            if (timeStr.Contains("T12:00") || timeStr.Contains(" 12:00"))
            {
                noonIndex = i;
                break;
            }
        }

        // If 12:00 not found, use the closest time or first available
        if (noonIndex == -1)
        {
            noonIndex = response.Hourly.Time.Count / 2; // Use middle of the day
        }

        var temperature = response.Hourly.Temperature2m[noonIndex];
        var precipitation = response.Hourly.Precipitation[noonIndex];

        return new WeatherResponseContract
        {
            Temperature = temperature,
            Precipitation = precipitation,
            Date = date,
            Location = $"Lat: {latitude:F2}, Lon: {longitude:F2}"
        };
    }

    private class OpenMeteoResponse
    {
        public HourlyData? Hourly { get; set; }
    }

    private class HourlyData
    {
        [JsonPropertyName("time")]
        public List<string> Time { get; set; } = new();
        
        [JsonPropertyName("temperature_2m")]
        public List<double> Temperature2m { get; set; } = new();
        
        [JsonPropertyName("precipitation")]
        public List<double> Precipitation { get; set; } = new();
    }
}

