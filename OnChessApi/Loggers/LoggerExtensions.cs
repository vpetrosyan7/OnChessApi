using OnChessApi.Repository;

namespace OnChessApi.Loggers
{
    public static class LoggerExtensions
    {
        public static ILoggingBuilder AddDB(this ILoggingBuilder builder, MySqlRepository mySqlRepository)
        {
            builder.AddProvider(new DBLoggerProvider(mySqlRepository));

            return builder;
        }
    }
}
