// VClinic.Infrastructure/Persistence/Configurations/EmpleadoConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VClinic.Domain.Entities;
using VClinic.Domain.Entities.Catalogos;

namespace VClinic.Infrastructure.Persistence.Configurations;

public class EmpleadoConfiguration : IEntityTypeConfiguration<Empleado>
{
    public void Configure(EntityTypeBuilder<Empleado> builder)
    {
        builder.ToTable("Empleados");

        builder.HasKey(e => e.IdEmpleado);

        builder.Property(e => e.CodigoEmpleado)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(e => e.IdDepartamento)
            .IsRequired();

        builder.Property(e => e.IdPosicion)
           .IsRequired();

        builder.Property(e => e.IdProfesion)
           .IsRequired();

        builder.HasIndex(e => e.CodigoEmpleado)
            .IsUnique();

        builder.HasOne(e => e.Persona)
            .WithOne(p => p.Empleado)
            .HasForeignKey<Empleado>(e => e.IdPersona)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Posiciones)
            .WithMany(u => u.Empleados)
            .HasForeignKey(p => p.IdPosicion)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Departamento)
            .WithMany(u => u.Empleados)
            .HasForeignKey(p => p.IdDepartamento)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Profesion)
           .WithMany(u => u.Empleados)
           .HasForeignKey(p => p.IdProfesion)
           .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(e => e.IdPersona)
            .IsUnique();
    }
}
