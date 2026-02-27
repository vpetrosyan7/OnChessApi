using OnChessApi.Repository;

namespace OnChessApi.Loggers
{
    public class DBLoggerProvider : ILoggerProvider
    {
        private readonly MySqlRepository _mySqlRepository;

        public DBLoggerProvider(MySqlRepository mySqlRepository)
        {
            _mySqlRepository = mySqlRepository;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new DBLogger(_mySqlRepository);
        }

        public void Dispose()
        {
        }
    }
}
