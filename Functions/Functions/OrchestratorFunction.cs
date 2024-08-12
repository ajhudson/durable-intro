using Functions.Extensions;
using Functions.Functions.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;
using Microsoft.Extensions.Logging;

namespace Functions.Functions;

public class OrchestratorFunction(ILogger<OrchestratorFunction> logger)
{
    [Function(nameof(GetDataOrchestrator))]
    public async Task GetDataOrchestrator([OrchestrationTrigger] TaskOrchestrationContext context)
    {
        List<string> citiesByChaining = ["manchester", "london", "edinburgh"];
        List<string> citiesByFanInFanOut = ["paris", "madrid", "rome", "berlin", "copenhagen", "oslo", "new-york", "riyadh", "mumbai", "beijing", "sydney", "buenos-aires"];
        List<int> temperatures = new();

        // Function chaining: This will loop through each city one-by-one
        foreach (var currentCity in citiesByChaining)
        {
            var weatherResponse = await context.CallActivityAsync<WeatherResponse>(nameof(WeatherActivityFunction.GetWeatherForCityActivity), currentCity);
            temperatures.Add(weatherResponse.Temperature.ExtractFirstInt());
        }
        
        // Fan-in Fan-out: This will start all activity tasks in parallel and we will wait for them all to finish
        var tasks = new Task<WeatherResponse>[citiesByFanInFanOut.Count];

        foreach (var (city, idx) in citiesByFanInFanOut.Select((city, idx) => (city, idx)))
        {
            tasks[idx] = context.CallActivityAsync<WeatherResponse>(nameof(WeatherActivityFunction.GetWeatherForCityActivity), city);
        }

        await Task.WhenAll(tasks);
        var allTemperatures = tasks.Select(t => t.Result.Temperature.ExtractFirstInt()).ToList();
        temperatures.AddRange(allTemperatures);
        
        logger.LogInformation("Average temperature is: {AverageTemperature}", temperatures.Average());
    }
}