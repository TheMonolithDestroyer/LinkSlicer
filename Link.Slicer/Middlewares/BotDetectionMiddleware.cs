using Microsoft.Extensions.Logging.Abstractions;
using System.Net;
using UAParser;

namespace Link.Slicer.Middlewares
{
    public class BotDetectionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<BotDetectionMiddleware> _logger;

        public BotDetectionMiddleware(RequestDelegate next, ILogger<BotDetectionMiddleware> logger = null)
        {
            _next = next;
            _logger = logger ?? NullLogger<BotDetectionMiddleware>.Instance;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var userAgent = context.Request.Headers["User-Agent"].FirstOrDefault();

            if (!string.IsNullOrEmpty(userAgent))
            {
                var parser = Parser.GetDefault();
                ClientInfo clientInfo = parser.Parse(userAgent);

                if (clientInfo.Device.IsSpider)
                {
                    _logger.LogInformation($"Request from bot: {userAgent}");

                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    return;
                }
            }

            await _next(context);
        }
    }
}
