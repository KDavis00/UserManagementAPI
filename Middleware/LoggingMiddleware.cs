using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace UserManagementAPI.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var method = context.Request.Method;
            var path = context.Request.Path;

            _logger.LogInformation("Incoming Request: {Method} {Path}", method, path);

            await _next(context);

            var statusCode = context.Response.StatusCode;
            _logger.LogInformation("Outgoing Response: {StatusCode}", statusCode);
        }
    }
}
