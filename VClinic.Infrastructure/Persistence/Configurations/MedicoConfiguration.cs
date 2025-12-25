// VClinic.Infrastructure/Persistence/Configurations/MedicoConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VClinic.Domain.Entities;

namespace VClinic.Infrastructure.Persistence.Configurations;

public class MedicoConfiguration : IEntityTypeConfiguration<Medico>
{
    public void Configure(EntityTypeBuilder<Medico> builder)
    {
        builder.ToTable("Medicos");

        builder.HasKey(m => m.IdMedico);

        builder.Property(m => m.NumeroColegiado)
            .IsRequired()
            .HasMaxLength(20);
                
        builder.HasIndex(m => m.NumeroColegiado)
            .IsUnique();

        builder.HasOne(m => m.Persona)
            .WithOne(p => p.Medico)
            .HasForeignKey<Medico>(m => m.IdPersona)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(m => m.IdPersona)
            .IsUnique();
    }
}
