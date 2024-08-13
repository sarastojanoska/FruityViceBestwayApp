using FruityViceBestwayApp.Entities;

namespace FruityViceBestwayApp.DataRepository
{
    public interface IExceptionLoggerRepository
    {
        Task AddLogExceptionAsync(ExceptionLog log);
    }
}
