using Microsoft.EntityFrameworkCore;
using Endeudator.Models;

namespace Endeudator.Data
{
    internal class Context : DbContext
    {
        public DbSet<Debt> Debts { get; set; }
        public DbSet<Movement> Movements { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<CurrentRate> CurrentRates { get; set; }
        public string ConnectionString { get; private set; }

        public Context()
        {
            //The connection string should be located in [App directory]/Config/ConnectionString.txt
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(baseDirectory, "Config", "ConnectionString.txt");
            ConnectionString = File.ReadAllText(filePath).Trim();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }
    }
}
