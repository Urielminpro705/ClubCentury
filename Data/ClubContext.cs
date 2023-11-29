using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClubCentury.Models;

namespace ClubCentury.Data
{
    public class ClubContext : DbContext
    {
        public ClubContext (DbContextOptions<ClubContext> options)
            : base(options)
        {
        }

        public DbSet<Socio> Socios { get; set; }
        public DbSet<Inscripcion> Inscripciones { get; set; }
        public DbSet<Taller> Talleres { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Paquete> Paquetes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Taller>().ToTable("Taller");
            modelBuilder.Entity<Inscripcion>().ToTable("Inscripcion");
            modelBuilder.Entity<Socio>().ToTable("Socio");
            modelBuilder.Entity<Empleado>().ToTable("Empleado");
            modelBuilder.Entity<Club>().ToTable("Club");
            modelBuilder.Entity<Paquete>().ToTable("Paquete");
        }
    }
}
