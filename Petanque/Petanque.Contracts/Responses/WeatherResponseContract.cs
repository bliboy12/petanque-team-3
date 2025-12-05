namespace Petanque.Contracts.Responses;

public class WeatherResponseContract
{
    public double Temperature { get; set; }
    public double Precipitation { get; set; }
    public string Location { get; set; } = string.Empty;
    public DateTime Date { get; set; }
}

