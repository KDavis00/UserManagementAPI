using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace UserManagementAPI.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuthenticationMiddleware> _logger;

        public AuthenticationMiddleware(RequestDelegate next, ILogger<AuthenticationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(token) || !token.StartsWith("Bearer ") || !IsValidToken(token.Substring(7)))
            {
                _logger.LogWarning("Unauthorized access attempt.");
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }

            await _next(context);
        }

        private bool IsValidToken(string token)
        {
            // Replace with real token validation logic
            return token == "TechHiveSecureToken";
        }
    }
}
