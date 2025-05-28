using Serilog.Context;

namespace NxEmailService.Middleware;

public class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;

    public CorrelationIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Request.Headers.TryGetValue("correlation-id-header", out var correlationIds);
        var correlationId = correlationIds.FirstOrDefault() ?? context.TraceIdentifier;

        using (LogContext.PushProperty("CorrelationId", correlationId))
        {
            await _next(context);
        }
    }
}
