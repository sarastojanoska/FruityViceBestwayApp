using FruityViceBestwayApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FruityViceBestwayApp.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Fruit> Fruits { get; set; }
        public DbSet<Nutrition> Nutritions { get; set; }
        public DbSet<Metadata> Metadata { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Fruit>()
                .HasOne(f => f.Nutrition)
                .WithOne(n => n.Fruit)
                .HasForeignKey<Nutrition>(n => n.FruitId);

            modelBuilder.Entity<Fruit>()
                .HasMany(f => f.Metadata)
                .WithOne(m => m.Fruit)
                .HasForeignKey(m => m.FruitId);

        }
    }
}
