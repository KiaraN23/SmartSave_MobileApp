using Microsoft.EntityFrameworkCore;
using SmartSave.Core.Entities;

namespace SmartSaveApp.Infrastructure.Data
{
    public class SmartSaveDbContext : DbContext
    {
        public SmartSaveDbContext(DbContextOptions<SmartSaveDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region "Table names"
            modelBuilder.Entity<User>().ToTable("Users");
            #endregion

            #region "PKs"
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            #endregion

            #region "Relationships"
            #endregion

            #region "Property Configurations"
            modelBuilder.Entity<User>().Property(user => user.FirstName)
                .IsRequired();

            modelBuilder.Entity<User>().Property(user => user.LastName)
                .IsRequired();

            modelBuilder.Entity<User>().Property(user => user.Email)
                .IsRequired();

            modelBuilder.Entity<User>().Property(user => user.Password)
                .IsRequired();
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
