using Domain.Entities;

namespace Domain.Interfaces.IServices
{
    public interface ILogService
    {
        LogOpenSearch LogDefault(string message, string stacktrace, string route, string loglevel);
        void LogException(string message);
        void Log(string message);
        void Warning(string message);
        string LogError(string errorType, string message);
    }
}
