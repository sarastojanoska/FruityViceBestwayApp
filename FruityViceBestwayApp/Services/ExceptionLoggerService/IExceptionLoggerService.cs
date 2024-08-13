namespace FruityViceBestwayApp.Services
{
    public interface IExceptionLoggerService
    {
        Task LogExceptionAsync(Exception ex);
    }
}
