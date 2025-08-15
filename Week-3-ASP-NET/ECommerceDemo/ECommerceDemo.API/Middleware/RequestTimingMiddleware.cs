using System.Diagnostics;

namespace ECommerceDemo.API;

public class RequestTimingMiddleware
{
    private readonly RequestDelegate _next; // A request delegate is just a function that can process an HTTPRequest
    private readonly ILogger<RequestTimingMiddleware> _logger;

    public RequestTimingMiddleware (RequestDelegate next, ILogger<RequestTimingMiddleware> logger)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var sw = Stopwatch.StartNew(); //When the method is called, we start our stopwatch
        await _next(context);
        sw.Stop(); //When the request resolves, we stop it

        _logger.LogInformation(
            "Request {Method} {Path} completed in {ElapsedMilliseconds}ms",
            context.Request.Method,
            context.Request.Path,
            sw.ElapsedMilliseconds
        );
    }
}
