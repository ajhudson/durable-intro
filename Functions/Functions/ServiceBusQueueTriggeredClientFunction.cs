using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;

namespace Functions.Functions;

public class ServiceBusQueueTriggeredClientFunction(ILogger<ServiceBusQueueTriggeredClientFunction> logger)
    : FunctionsBase
{
    private readonly ILogger<ServiceBusQueueTriggeredClientFunction> _logger = logger;

    [Function(nameof(ServiceBusQueueTriggeredClientFunction))]
    public async Task Run([ServiceBusTrigger("input-queue", Connection = ConnectionString)] string queueMessage, [DurableClient] DurableTaskClient client)
    {
        var instanceId = await client.ScheduleNewOrchestrationInstanceAsync("StartDoingStuff");
        this._logger.LogInformation("Scheduled orchestration {InstanceId}", instanceId);
        await client.WaitForInstanceStartAsync(instanceId);
        this._logger.LogInformation("Orchestration {InstanceId} has started", instanceId);
    }
}