using OnChessApi.ExceptionHandlers;

namespace OnChessApi.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseException(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandling>();
        }
    }
}
