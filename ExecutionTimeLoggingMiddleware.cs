namespace ShortCircuit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

public class ExecutionTimeLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ExecutionTimeLoggingMiddleware(RequestDelegate next, ILogger<ExecutionTimeLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        await _next(context);

        stopwatch.Stop();
        _logger.LogInformation($"Execution Time: {stopwatch.ElapsedMilliseconds} ms for {context.Request.Method} {context.Request.Path}");
    }
}
