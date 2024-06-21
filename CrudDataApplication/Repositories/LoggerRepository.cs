using CrudDataApplication.Interfaces;
using Newtonsoft.Json;

namespace CrudDataApplication.Repositories
{
    public class LoggerRepository<T> : ILoggerRepository<T> where T : class
    {
        private readonly ILogger<T> _logger;

        public LoggerRepository(ILogger<T> logger)
        {
            _logger = logger;
        }
        public void ErrorMessage(object errorMessage)
        {
            string errorMessages = JsonConvert.SerializeObject(errorMessage, Formatting.Indented);
            _logger.LogError(errorMessages);
        }

        public void InfoMessage(string message)
        {
            _logger.LogInformation(message);
        }

        public void InfoWithObjectMessage(object objectMessage)
        {
            string infoObjectMessages = JsonConvert.SerializeObject(objectMessage, Formatting.Indented);
            _logger.LogInformation(infoObjectMessages);
        }
    }
}
