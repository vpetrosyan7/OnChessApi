namespace OnChessApi.ExceptionHandlers
{
    public class ExceptionHandling
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandling(RequestDelegate next, ILogger<ExceptionHandling> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Handle the exception
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    error = ex.Message,
                    stackTrace = ex.StackTrace
                };

                _logger.LogError(ex.StackTrace);

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
