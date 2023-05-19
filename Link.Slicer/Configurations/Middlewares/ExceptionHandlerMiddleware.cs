using Link.Slicer.Application.Common.Interfaces;
using Link.Slicer.Application.Exceptions;
using Link.Slicer.Application.Models;
using Newtonsoft.Json;
using System.Net;

namespace Link.Slicer.Configurations.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IApplicationLogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, IApplicationLogger<ExceptionHandlerMiddleware> logger = null)
        {
            _next = next;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var statusCode = HttpStatusCode.InternalServerError;
                var message = ex.Message;
                if (ex is NotFoundException notFoundException)
                {
                    statusCode = notFoundException.StatusCode;
                    message = notFoundException.Message;
                }
                var exceptionResult = JsonConvert.SerializeObject(Result.Fail(message, statusCode));

                _logger.LogError(exceptionResult, ex);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)statusCode;
                await context.Response.WriteAsync(exceptionResult);
            }
        }
    }
}
