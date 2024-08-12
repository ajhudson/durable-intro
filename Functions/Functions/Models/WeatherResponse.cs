using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Functions.Functions.Models;

public class WeatherResponse
{
    [JsonPropertyName("temperature")] 
    public string Temperature { get; set; } = string.Empty;

    [JsonPropertyName("wind")]
    public string Wind { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("forecast")]
    public List<Forecast> Forecast { get; set; }
}

public class Forecast
{
    [JsonPropertyName("day")]
    public string Day { get; set; } = string.Empty;

    [JsonPropertyName("temperature")]
    public string Temperature { get; set; } = string.Empty;

    [JsonPropertyName("wind")]
    public string Wind { get; set; } = string.Empty;
}