using Microsoft.AspNetCore.Mvc;
using Petanque.Contracts.Responses;
using Petanque.Services.Interfaces;

namespace Petanque.Api.Controllers;

[ApiController]
[Route("api/weather")]
public class WeatherController(IWeatherService weatherService) : Controller
{
    [HttpGet("forecast")]
    public async Task<ActionResult<WeatherResponseContract>> GetWeatherForecast(
        [FromQuery] DateTime date,
        [FromQuery] double latitude,
        [FromQuery] double longitude)
    {
        try
        {
            var weather = await weatherService.GetWeatherForecastAsync(date, latitude, longitude);
            return Ok(weather);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error fetching weather: {ex.Message}");
        }
    }
}

