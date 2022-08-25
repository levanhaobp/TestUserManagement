using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using static Domain.Helpers.Enums;

namespace Infrastructure.Contexts
{
    public class ApplicationContext : DbContext, IApplicationContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasData(
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "admin",
                        Name = "Admin",
                        Password = "123123",
                        Email = "levanhaobp@gmail.com",
                        Role = (sbyte)ERole.ADMIN
                    },
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "levanhaobp",
                        Name = "Hao Le",
                        Password = "123123",
                        Email = "levanhaobp@gmail.com",
                        Role = (sbyte)ERole.USER
                    }
                );
        }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
        public DbSet<User> Users { get; set; }
    }
}
