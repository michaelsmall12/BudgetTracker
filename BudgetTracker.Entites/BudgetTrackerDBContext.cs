using BudgetTracker.Entites;
using Microsoft.EntityFrameworkCore;


namespace BudgetTracker.Services
{
    public class BudgetTrackerDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("DataBaseConnectionString"));
        }
    }
}
