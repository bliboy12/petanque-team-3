using Petanque.Services.Services;
using System;
using System.Collections.Generic;
using System.Text;
using static Petanque.Services.Services.WeatherService;

namespace Petanque.Services.Api {
    public interface IWeatherApiClient {
        Task<OpenMeteoResponse?> GetWeatherAsync(string url);
    }
}
