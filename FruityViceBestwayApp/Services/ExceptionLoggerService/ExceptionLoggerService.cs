using FruityViceBestwayApp.Data;
using FruityViceBestwayApp.DataRepository;
using FruityViceBestwayApp.Entities;

namespace FruityViceBestwayApp.Services
{
    public class ExceptionLoggerService : IExceptionLoggerService
    {
        private readonly IExceptionLoggerRepository _loggerRepository;

        public ExceptionLoggerService(IExceptionLoggerRepository loggerRepository)
        {
            _loggerRepository = loggerRepository;
        }

        public async Task LogExceptionAsync(Exception ex)
        {
            var exceptionLog = new ExceptionLog
            {
                ExceptionMessage = ex.Message,
                StackTrace = ex.StackTrace,
                DateOccurred = DateTime.UtcNow
            };
            await _loggerRepository.AddLogExceptionAsync(exceptionLog);
        }
    }
}
