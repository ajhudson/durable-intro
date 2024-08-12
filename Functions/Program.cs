using System.Reflection.Metadata;
using Functions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddHttpClient(Constants.HttpNamedClients.WeatherHttpClient, client =>
        {
            client.BaseAddress = new Uri("https://goweather.herokuapp.com");
        });
    })
    .Build();

host.Run();