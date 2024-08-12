using System.Text.Json;
using Functions.Functions.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Functions.Extensions;

namespace Functions.Functions;

public class WeatherActivityFunction(IHttpClientFactory httpClientFactory, ILogger<WeatherActivityFunction> logger) : FunctionsBase
{
    [Function(nameof(GetWeatherForCityActivity))]
    public async Task<WeatherResponse?> GetWeatherForCityActivity([ActivityTrigger] string cityName, FunctionContext ctx)
    {
        var httpClient = httpClientFactory.CreateClient(Constants.HttpNamedClients.WeatherHttpClient);
        var response = await httpClient.GetAsync($"/weather/{cityName}");
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var weatherResponse = JsonSerializer.Deserialize<WeatherResponse>(jsonResponse);

        return weatherResponse;
    }
}