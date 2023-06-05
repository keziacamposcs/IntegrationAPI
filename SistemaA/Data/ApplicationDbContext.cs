using System;
using Microsoft.EntityFrameworkCore;
using SistemaA.Models;

namespace SistemaA.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Employee
        public DbSet<EmployeeModel> Employee { get; set; }

        // Department
        public DbSet<DepartamentModel> Departament { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeModel>()
                .HasOne(e => e.Departament)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartamentId);

            base.OnModelCreating(modelBuilder);
        }

    }

}

