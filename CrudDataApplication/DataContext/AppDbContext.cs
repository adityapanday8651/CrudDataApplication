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
        public DbSet<Medicine> Medicines { get; set; }

        //Another Projects
        public DbSet<Company> Company { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Tasks> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Roles>().HasData(
                new Roles { Id = 1, RoleName = "Admin" },
                new Roles { Id = 2, RoleName = "User" },
                new Roles { Id = 3, RoleName = "ReadOnly" }
            );
            modelBuilder.Entity<Medicine>()
               .Property(p => p.Price)
               .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Register>()
               .HasOne(p => p.Roles)
               .WithMany(c => c.Register)
               .HasForeignKey(p => p.RoleId);



            // Projects With Tasks ForeignKey
            modelBuilder.Entity<Projects>()
              .HasOne(p => p.Tasks)
              .WithMany(c => c.Projects)
              .HasForeignKey(p => p.TasksId);

            // Departments with Manager ForeignKey
            modelBuilder.Entity<Departments>()
              .HasOne(p => p.Manager)
              .WithMany(c => c.Departments)
              .HasForeignKey(p => p.ManagerId);


            // Departments with Employees ForeignKey
            modelBuilder.Entity<Departments>()
              .HasOne(p => p.Employees)
              .WithMany(c => c.Departments)
              .HasForeignKey(p => p.EmployeesId);

            // Departments with Projects ForeignKey
            modelBuilder.Entity<Departments>()
              .HasOne(p => p.Projects)
              .WithMany(c => c.Departments)
              .HasForeignKey(p => p.ProjectsId);



            //Company with  Departments
            modelBuilder.Entity<Company>()
              .HasOne(p => p.Departments)
              .WithMany(c => c.Company)
              .HasForeignKey(p => p.DepartmentId);


            //Employee with  Address
            modelBuilder.Entity<Employees>()
              .HasOne(p => p.Address)
              .WithMany(c => c.Employees)
              .HasForeignKey(p => p.AddressId);





           
        }
    }
}
