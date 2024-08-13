using FruityViceBestwayApp.Data;
using FruityViceBestwayApp.Entities;

namespace FruityViceBestwayApp.DataRepository
{
    public class ExceptionLoggerRepository : IExceptionLoggerRepository
    {
        private readonly ExceptionDbContext _context;

        public ExceptionLoggerRepository(ExceptionDbContext context)
        {
            _context = context;
        }

        public async Task AddLogExceptionAsync(ExceptionLog log)
        {
            _context.ExceptionLogs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}
