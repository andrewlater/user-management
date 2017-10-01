using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using UserManagement.Core.Models;

namespace UserManagement.Core.Data
{
    public class UserManagementContext : DbContext
    {
        public UserManagementContext(DbContextOptions<UserManagementContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .Property(b => b.Username)
                .IsRequired();

            builder.Entity<User>()
                .HasIndex(b => b.Username)
                .IsUnique();

            builder.Entity<User>()
                .Property(b => b.CreateDate)
                .IsRequired();

            builder.Entity<User>()
                .Property(b => b.LastUpdateDate)
                .IsRequired();

            builder.Entity<User>()
                .Property(b => b.Hash)
                .IsRequired();

            builder.Entity<Role>()
                .Property(b => b.Name)
                .IsRequired();

            builder.Entity<UserRole>()
                .HasIndex(ur => new { ur.UserId, ur.RoleId })
                .IsUnique();
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            var now = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Added))
            {
                var auditableEntry = entry.Entity as IAuditable;

                if (auditableEntry == null) continue;

                auditableEntry.CreateDate = now;
                auditableEntry.LastUpdateDate = now;
            }

            foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Modified))
            {
                var auditableEntry = entry.Entity as IAuditable;

                if (auditableEntry == null) continue;

                auditableEntry.LastUpdateDate = now;
            }

            return base.SaveChanges();
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}
