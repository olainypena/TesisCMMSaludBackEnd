using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VClinic.Domain.Entities;
using VClinic.Domain.Entities.Catalogos;
using VClinic.Infrastructure.Persistence.Configurations;

namespace VClinic.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<CentroMedico> CentroMedico => Set<CentroMedico>();
        public DbSet<DatosPersona> Personas => Set<DatosPersona>();
        public DbSet<Paciente> Pacientes => Set<Paciente>();
        public DbSet<Empleado> Empleados => Set<Empleado>();
        public DbSet<Medico> Medicos => Set<Medico>();

        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<EstadoCivil> EstadoCivils { get; set; }
        public DbSet<GrupoSanguinio> GrupoSanguinios { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Ocupacion> Ocupacions { get; set; }
        public DbSet<Posiciones> Posiciones { get; set; }
        public DbSet<Profesion> Profesions { get; set; }
        public DbSet<TipoEquipamento> TipoEquipamentos { get; set; }
        public DbSet<TipoIdentificacion> TipoIdentificacions { get; set; }
        public DbSet<Equipamento> Equipamentos { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new CentroMedicoConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
