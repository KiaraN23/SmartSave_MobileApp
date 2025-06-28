using Microsoft.EntityFrameworkCore;
using SmartSave.Core.Entities;

namespace SmartSaveApp.Infrastructure.Data
{
    public class SmartSaveDbContext : DbContext
    {
        public SmartSaveDbContext(DbContextOptions<SmartSaveDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Goal> Goals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region "Table names"
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Goal>().ToTable("Goals");
            #endregion

            #region "PKs"
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<Goal>().HasKey(g => g.Id);
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

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
