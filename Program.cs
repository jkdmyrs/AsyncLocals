using AsyncLocals;
using AsyncLocals.RealExample.Application;
using AsyncLocals.RealExample.Audit;
using AsyncLocals.RealExample.Replay;
using Microsoft.Extensions.DependencyInjection;


// Run ONE section at a time.
//
// Think of this like a singleton service in a larger app:
// shared fields act like shared singleton state, while AsyncLocal keeps per-operation context.
// AsyncLocal is great for context flow, but not a general replacement for method parameters.



// Non-AsyncLocal example:
// Both jobs share the same values, so they can overwrite each other.
// That can make the printed results look mixed up.
for (var iteration = 1; iteration <= 5; iteration++)
{
    Console.WriteLine($"Non-AsyncLocal example - iteration {iteration}");

    var sourdoughTask = BreadMaker.BakeBread("Sourdough", 50);
    var ryeTask = BreadMaker.BakeBread("Rye", 40);

    await Task.WhenAll(sourdoughTask, ryeTask);
    Console.WriteLine();
}







Console.WriteLine();
Console.WriteLine("Press enter to continue to next example...");
Console.ReadLine();



// AsyncLocal example:
// Each bake job keeps its own values, even when both jobs run at the same time.
for (var iteration = 1; iteration <= 5; iteration++)
{
    Console.WriteLine($"AsyncLocal example - iteration {iteration}");

    var sourdoughTaskAsyncLocal = BreadMakerAsyncLocal.BakeBread("Sourdough", 50);
    var ryeTaskAsyncLocal = BreadMakerAsyncLocal.BakeBread("Rye", 40);

    await Task.WhenAll(sourdoughTaskAsyncLocal, ryeTaskAsyncLocal);
    Console.WriteLine();
}







Console.WriteLine();
Console.WriteLine("Press enter to continue to next example...");
Console.ReadLine();



// "Real World" example
// more realistic example of AsyncLocal usage in a DI scenario similar to DISIntegrator
IServiceProvider serviceProvider = new ServiceCollection()
    .AddSingleton<AuditHandler>()
    .AddSingleton<ExampleHandler>()
    .BuildServiceProvider();

// imagine this is a function Trigger
async Task FunctionAsync(string eventData, string replayId, string replayOf)
{
    // Get the ExampleHandler from the DI container
    // (in a real app, this would be injected into the function)
    var exampleHandler = serviceProvider.GetRequiredService<ExampleHandler>();

    // capture the replay context for this function invocation
    ReplayContextIngress.Capture(replayId, replayOf);

    // call the ExampleHandler, which will log an audit record
    await exampleHandler.ExecuteAsync(eventData);
}



for (var iteration = 1; iteration <= 5; iteration++)
{
    Console.WriteLine($"Real World example - iteration {iteration}");

    var e1 = FunctionAsync("Event 1", "ReplayId1", "ReplayOf1");
    var e2 = FunctionAsync("Event 2", "ReplayId2", "ReplayOf2");
    var e3 = FunctionAsync("Event 3", "ReplayId3", "ReplayOf3");

    await Task.WhenAll(e1, e2, e3);
    Console.WriteLine();
}

