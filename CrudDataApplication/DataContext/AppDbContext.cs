using CrudDataApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CrudDataApplication.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Register> Register { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Roles>().HasData(
                new Roles { Id = 1, RoleName = "Admin" },
                new Roles { Id = 2, RoleName = "User" },
                new Roles { Id = 3, RoleName = "ReadOnly" }
            );


            // Define precision and scale for the Price property
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");  // Define the SQL type for the Price property

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Register>()
                .HasOne(p => p.Roles)
                .WithMany(c => c.Register)
                .HasForeignKey(p => p.RoleId);


            // Refresh Tokens
            modelBuilder.Entity<RefreshToken>()
           .HasKey(r => r.Id);

            modelBuilder.Entity<RefreshToken>()
                .Property(r => r.Token)
                .IsRequired();

            modelBuilder.Entity<RefreshToken>()
                .Property(r => r.JwtId)
                .IsRequired();

            modelBuilder.Entity<RefreshToken>()
                .Property(r => r.CreationDate)
                .IsRequired();

            modelBuilder.Entity<RefreshToken>()
                .Property(r => r.ExpiryDate)
                .IsRequired();

            modelBuilder.Entity<RefreshToken>()
                .Property(r => r.UserName)
                .IsRequired();
        }

    }
}
