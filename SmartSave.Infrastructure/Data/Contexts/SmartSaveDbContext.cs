using Microsoft.EntityFrameworkCore;
using SmartSave.Core.Entities;

namespace SmartSave.Infrastructure.Data.Contexts
{
    public class SmartSaveDbContext : DbContext
    {
        public SmartSaveDbContext(DbContextOptions<SmartSaveDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region "Table names"
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Transaction>().ToTable("Transactions");
            #endregion

            #region "PKs"
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<Transaction>().HasKey(t => t.Id);
            #endregion

            #region "Relationships"
            #endregion

            #region "Property Configurations"
            #region "User"
            modelBuilder.Entity<User>().Property(user => user.FirstName)
                .IsRequired();

            modelBuilder.Entity<User>().Property(user => user.LastName)
                .IsRequired();

            modelBuilder.Entity<User>().Property(user => user.Email)
                .IsRequired();

            modelBuilder.Entity<User>().Property(user => user.Password)
                .IsRequired();
            #endregion

            #region "Transaction" 
            modelBuilder.Entity<Transaction>().Property(t => t.Date)
                .IsRequired();

            modelBuilder.Entity<Transaction>().Property(t => t.UserId)
                .IsRequired();

            modelBuilder.Entity<Transaction>().Property(t => t.Amount)
                .IsRequired();

            modelBuilder.Entity<Transaction>().Property(t => t.Amount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Transaction>().Property(t => t.Description)
                .IsRequired();

            modelBuilder.Entity<Transaction>().Property(t => t.Type)
                .IsRequired();
            #endregion

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
