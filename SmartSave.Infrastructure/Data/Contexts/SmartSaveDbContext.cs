using Microsoft.EntityFrameworkCore;
using SmartSave.Core.Entities;

namespace SmartSave.Infrastructure.Data.Contexts
{
    public class SmartSaveDbContext : DbContext
    {
        public SmartSaveDbContext(DbContextOptions<SmartSaveDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region "Table names"
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Goal>().ToTable("Goals");
            modelBuilder.Entity<Transaction>().ToTable("Transactions");
            modelBuilder.Entity<Suggestion>().ToTable("Suggestions");
            #endregion

            #region "PKs"
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<Goal>().HasKey(g => g.Id);
            modelBuilder.Entity<Transaction>().HasKey(t => t.Id);
            modelBuilder.Entity<Suggestion>().HasKey(t => t.Id);
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
            
            #region "Goals" 
            modelBuilder.Entity<Goal>().Property(t => t.UserId)
                .IsRequired();

            modelBuilder.Entity<Goal>().Property(t => t.Name)
                .IsRequired();

            modelBuilder.Entity<Goal>().Property(t => t.ObjectiveAmount)
                .IsRequired();

            modelBuilder.Entity<Goal>().Property(t => t.CurrentAmount)
                .IsRequired();

            modelBuilder.Entity<Goal>().Property(t => t.Deadline)
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

            #region "Suggestion"
            modelBuilder.Entity<Suggestion>().Property(t => t.UserId)
                .IsRequired();

            modelBuilder.Entity<Suggestion>().Property(t => t.SuggestionMessage)
                .IsRequired();

            modelBuilder.Entity<Suggestion>().Property(t => t.CreatedAt)
                .IsRequired();
            #endregion

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
