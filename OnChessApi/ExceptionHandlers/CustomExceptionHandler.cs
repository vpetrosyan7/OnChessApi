using Microsoft.AspNetCore.Diagnostics;

namespace OnChessApi.ExceptionHandlers
{
    public class CustomExceptionHandler : IExceptionHandler
    {
        private readonly ILogger _logger;

        public CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) 
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            httpContext.Response.ContentType = "application/json";

            var response = new
            {
                error = exception.Message,
                stackTrace = exception.StackTrace
            };

            _logger.LogError(exception.StackTrace);

            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

            return true; // Exception is handled
        }
    }
}
