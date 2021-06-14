using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace FirmaApi.Models
{
    public partial class FirmaDbContext : DbContext
    {
        public FirmaDbContext()
        {
        }

        public FirmaDbContext(DbContextOptions<FirmaDbContext> options)
            : base(options)
        {      
        }

        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Division> Divisions { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<WorkAssignment> WorkAssignments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<WorkAssignment>().ToTable("WorkAssignment");
            modelBuilder.Entity<Division>().ToTable("Division");
            modelBuilder.Entity<Project>().ToTable("Project");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Company>().ToTable("Company");


            modelBuilder.Entity<Employee>().HasKey(p => p.Id);

            modelBuilder.Entity<Employee>().Property(p => p.Id).IsRequired();

            modelBuilder.Entity<Employee>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

            modelBuilder.Entity<Employee>().Property(p => p.Surname).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Employee>().Property(p => p.LastName).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Employee>().Property(p => p.SignInDate).IsRequired();
            modelBuilder.Entity<Employee>().Property(p => p.PhoneNumber).HasMaxLength(12);
            modelBuilder.Entity<Employee>().Property(p => p.Mail).HasMaxLength(50);
            modelBuilder.Entity<Employee>().Property(p => p.Title).HasMaxLength(20);

            modelBuilder.Entity<WorkAssignment>().HasKey(p => p.Id);

            modelBuilder.Entity<WorkAssignment>().Property(p => p.Id).IsRequired();

            modelBuilder.Entity<WorkAssignment>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

            modelBuilder.Entity<WorkAssignment>().Property(p => p.EmployeeId).IsRequired();
            modelBuilder.Entity<WorkAssignment>().Property(p => p.DepartmentId).IsRequired();
            modelBuilder.Entity<WorkAssignment>().Property(p => p.StartDate).IsRequired();            


            modelBuilder.Entity<Department>().HasKey(p => p.Id);

            modelBuilder.Entity<Department>().Property(p => p.Id).IsRequired();

            modelBuilder.Entity<Department>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

            modelBuilder.Entity<Department>().Property(p => p.LeaderId).IsRequired();
            modelBuilder.Entity<Department>().Property(p => p.Name).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Department>().Property(p => p.ProjectId).IsRequired();
            modelBuilder.Entity<Department>().Property(p => p.PhoneNumber).HasMaxLength(50);

            modelBuilder.Entity<Project>().HasKey(p => p.Id);

            modelBuilder.Entity<Project>().Property(p => p.Id).IsRequired();

            modelBuilder.Entity<Project>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

            modelBuilder.Entity<Project>().Property(p => p.LeaderId).IsRequired();
            modelBuilder.Entity<Project>().Property(p => p.Name).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Project>().Property(p => p.DivisionId).IsRequired();
            modelBuilder.Entity<Project>().Property(p => p.PhoneNumber).HasMaxLength(50);

            modelBuilder.Entity<Division>().HasKey(p => p.Id);

            modelBuilder.Entity<Division>().Property(p => p.Id).IsRequired();

            modelBuilder.Entity<Division>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

            modelBuilder.Entity<Division>().Property(p => p.LeaderId).IsRequired();
            modelBuilder.Entity<Division>().Property(p => p.Name).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Division>().Property(p => p.CompanyId).IsRequired();
            modelBuilder.Entity<Division>().Property(p => p.PhoneNumber).HasMaxLength(50);

            modelBuilder.Entity<Company>().HasKey(p => p.Id);

            modelBuilder.Entity<Company>().Property(p => p.Id).IsRequired();

            modelBuilder.Entity<Company>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

            modelBuilder.Entity<Company>().Property(p => p.LeaderId).IsRequired();
            modelBuilder.Entity<Company>().Property(p => p.Name).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Company>().Property(p => p.PhoneNumber).HasMaxLength(50);
            
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
