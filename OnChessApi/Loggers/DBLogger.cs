using OnChessApi.Repository;

namespace OnChessApi.Loggers
{
    public class DBLogger : ILogger, IDisposable
    {
        static private object _lock = new();
        private readonly MySqlRepository _mySqlRepository;

        public DBLogger(MySqlRepository mySqlRepository)
        {
            _mySqlRepository = mySqlRepository;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return this;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            lock (_lock)
            {
                _mySqlRepository.AddLog(formatter(state, exception));
            }
        }

        public void Dispose()
        {
        }
    }
}
