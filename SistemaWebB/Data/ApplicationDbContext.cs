using System;
using SistemaWebB.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace SistemaWebB.Data
{
	public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<FuncionarioModel> Funcionarios { get; set; }
        public DbSet<CargoModel> Cargos { get; set; }
        public DbSet<SetorModel> Setores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FuncionarioModel>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<CargoModel>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<SetorModel>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<CargoModel>()
                .HasMany(c => c.Funcionarios)
                .WithOne(f => f.Cargo)
                .HasForeignKey(f => f.CargoId);

            modelBuilder.Entity<SetorModel>()
                .HasMany(s => s.Funcionarios)
                .WithOne(f => f.Setor)
                .HasForeignKey(f => f.SetorId);
        }

    }
}

