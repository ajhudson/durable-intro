using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.DurableTask;

namespace Functions.Functions;

public class OrchestratorFunction
{
    [FunctionName(nameof(StartDoingStuff))]
    public async Task StartDoingStuff([OrchestrationTrigger] TaskOrchestrationContext context)
    {
        await Task.CompletedTask;
    }
}