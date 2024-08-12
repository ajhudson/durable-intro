using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;

namespace Functions.Functions;

public class TimerTriggeredClientFunction(ILogger<TimerTriggeredClientFunction> logger)
    : FunctionsBase
{
    [Function(nameof(TimerTriggeredClientFunction))]
    public async Task Run([TimerTrigger("0 0 * * * *")] string queueMessage, [DurableClient] DurableTaskClient client)
    {
        var instanceId = await client.ScheduleNewOrchestrationInstanceAsync(nameof(OrchestratorFunction.GetDataOrchestrator));
        logger.LogInformation("Scheduled orchestration {InstanceId}", instanceId);
        await client.WaitForInstanceStartAsync(instanceId);
        logger.LogInformation("Orchestration {InstanceId} has started", instanceId);
    }
}