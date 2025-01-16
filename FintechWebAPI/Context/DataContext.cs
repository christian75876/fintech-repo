using FintechWebAPI.Class;
using Microsoft.EntityFrameworkCore;

namespace FintechWebAPI.Entity
{
    public class DataContext : DbContext 
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
        
        }

        public DbSet<Cuenta> cuentas { get; set; }
        public DbSet<FintechTransaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FintechTransaction>()
                .HasOne(t => t.OriginAccount)
                .WithMany(c => c.OriginTransactions)
                .HasForeignKey(t => t.OriginAccountId)
                .OnDelete(DeleteBehavior.Restrict);  

            modelBuilder.Entity<FintechTransaction>()
                .HasOne(t => t.DestinationAccount)
                .WithMany(c => c.DestinationTransactions)
                .HasForeignKey(t => t.DestinationAccountId)
                .OnDelete(DeleteBehavior.Restrict);  
        }
    }
}
