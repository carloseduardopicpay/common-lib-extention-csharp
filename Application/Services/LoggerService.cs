using Domain.Entities;
using Domain.Interfaces.IServices;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace Application.Services
{
    public class LoggerService : ILogService
    {
        private readonly ILogger<LoggerService> _logger;


        public LoggerService(ILogger<LoggerService> logger)
        {
            _logger = logger;
        }

        public void Log(string message)
        {
            Console.Write($"Requisicao atual : {message} \n");
        }

        public LogOpenSearch LogDefault(string message, string stacktrace, string route, string loglevel)
        {
            var LogOpenSearch = new LogOpenSearch()
            {
                log_level = loglevel,
                @class = route,
                message = message,
                stack_trace = stacktrace,
            };

            var jsonRetorno = JsonConvert.SerializeObject(LogOpenSearch);
            Log(jsonRetorno);

            return LogOpenSearch;
        }


        public string LogError(string errorType, string message)
        {
            Console.Write($"Error {errorType}: {message}");
            return $"Error {errorType}: {message}";
        }

        public void Warning(string message)
        {
            Console.Write($"Logged message: {message}");
        }


        public void LogException(string message)
        {
            string fileName = string.Format("{0}_{1}.log", "Exception", DateTime.Now.ToShortDateString());
            string logFilePath = string.Format(@"{0}\{1}", AppDomain.CurrentDomain.BaseDirectory, fileName);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("----------------------------------------");
            sb.AppendLine(DateTime.Now.ToString());
            sb.AppendLine(message);
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.Write(sb.ToString());
                writer.Flush();
            }
        }

    }
}
