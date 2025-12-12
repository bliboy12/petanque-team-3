using Petanque.Services.Services;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using static Petanque.Services.Services.WeatherService;

namespace Petanque.Services.Api {
    public class WeatherApiClient : IWeatherApiClient {
        private readonly HttpClient _httpClient;

        public WeatherApiClient(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public Task<OpenMeteoResponse?> GetWeatherAsync(string url) {
            return _httpClient.GetFromJsonAsync<WeatherService.OpenMeteoResponse>(url);
        }
    }
}
