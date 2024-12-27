using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace CleanArchitecture.Infrastructure.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<AdminUserUtility> AdminUserUtilities { get; set; }
        public DbSet<AdminUserRole> AdminUserRoles { get; set; }
        public DbSet<UserUtility> UserUtilitys { get; set; }
        public DbSet<AdminRole> AdminRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AdminUser>()
                        .HasIndex(u => u.Email)
                        .IsUnique(); // Enforces the unique constraint

            modelBuilder.Entity<AdminUser>()
                        .HasIndex(u => u.Username)
                        .IsUnique(); // Enforces the unique constraint

            modelBuilder.Entity<AdminUserUtility>()
                        .HasOne(u => u.AdminUser)
                        .WithOne(a => a.AdminUserUtilities)
                        .HasForeignKey<AdminUserUtility>(u => u.UserId);

            modelBuilder.Entity<AdminUserRole>()
                        .HasOne(a => a.AdminUser)
                        .WithMany(u => u.AdminUserRoles)
                        .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<AdminUserRole>()
                        .HasOne(a => a.AdminRole)
                        .WithMany(r => r.AdminUserRoles)
                        .HasForeignKey(a => a.RoleId);
        }
    }
}
