using FruityViceBestwayApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace FruityViceBestwayApp.Data
{
    public class ExceptionDbContext: DbContext
    {
        public ExceptionDbContext(DbContextOptions<ExceptionDbContext> options)
            : base(options)
        {
        }

        public DbSet<ExceptionLog> ExceptionLogs { get; set; }

    }
}
