using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BlogService
{
    public class APIKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        // private IApiKeyService _service;

        public APIKeyMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)//, IApiKeyService service)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<APIKeyMiddleware>();
            //   _service = service
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation("Handling API key for: " + context.Request.Path);
            // console.logion("Handling API key for: " + context.Request.Path);
           // if (context.Request.Headers.Values.Contains(new Microsoft.Extensions.Primitives.StringValues("Name:arminder")))
                await _next.Invoke(context);
            //else


                _logger.LogInformation("Finished handling api key.");
        }
    }

    public static class ApiKeyExtensions
    {
        public static IApplicationBuilder UseApiKey(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<APIKeyMiddleware>();
        }
    }
}
