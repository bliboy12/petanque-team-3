using Petanque.Contracts.Responses;

namespace Petanque.Services.Interfaces;

public interface IWeatherService
{
    Task<WeatherResponseContract> GetWeatherForecastAsync(DateTime date, double latitude, double longitude);
}

