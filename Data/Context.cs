using Microsoft.EntityFrameworkCore;
using Endeudator.Models;
using Microsoft.EntityFrameworkCore.Design;

namespace Endeudator.Data
{
    internal class Context : DbContext
    {
        public DbSet<Debt> Debts { get; set; }
        public DbSet<Movement> Movements { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<CurrentRate> CurrentRates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"
                    Server=localhost; 
                    Database=Endeudator; 
                    Trusted_Connection=True; 
                    Trust Server Certificate=Yes; 
                    User Id=Endeudator; 
                    password=Endeudator");
        }
    }
}
