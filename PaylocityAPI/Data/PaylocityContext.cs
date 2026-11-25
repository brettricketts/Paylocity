using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaylocityAPI.Models;
using PaylocityAPI.ViewModels;

namespace PaylocityAPI.Data
{
    public class PaylocityContext : DbContext
    {
        public PaylocityContext(DbContextOptions<PaylocityContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.EnableDetailedErrors(true);
        }

        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Dependent> Dependent { get; set; }
        public virtual DbSet<CalculationParameter> CalculationParameter { get; set; }



        public virtual DbSet<EmployeeCostsVM> EmployeeCostsVM { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Dependent>().ToTable("Dependent");
            modelBuilder.Entity<CalculationParameter>().ToTable("CalculationParameter");


            modelBuilder.Entity<EmployeeCostsVM>().ToView("EmployeeCostsVM").HasNoKey();

        }

    }
}
