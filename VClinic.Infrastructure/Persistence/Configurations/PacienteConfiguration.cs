using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VClinic.Domain.Entities;

namespace VClinic.Infrastructure.Persistence.Configurations;

public class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
{
    public void Configure(EntityTypeBuilder<Paciente> builder)
    {
        builder.ToTable("Pacientes");

        builder.HasKey(p => p.IdPaciente);

        builder.Property(p => p.IdHistorial)
            .IsRequired();

        builder.HasIndex(p => p.IdHistorial)
            .IsUnique();

        builder.HasOne(p => p.Persona)
            .WithOne(pers => pers.Paciente)
            .HasForeignKey<Paciente>(p => p.IdPersona)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(p => p.IdPersona)
            .IsUnique();
    }
}

