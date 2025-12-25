// VClinic.Infrastructure/Persistence/Configurations/PersonaConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VClinic.Domain.Entities;

namespace VClinic.Infrastructure.Persistence.Configurations;

public class PersonaConfiguration : IEntityTypeConfiguration<DatosPersona>
{
    public void Configure(EntityTypeBuilder<DatosPersona> builder)
    {
        builder.ToTable("Personas");

        builder.HasKey(p => p.IdPersona);

        builder.Property(p => p.Nombres)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Apellidos)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Email)
            .HasMaxLength(100);

        builder.Property(p => p.Telefono)
            .HasMaxLength(20);

        builder.Property(p => p.Celular)
            .HasMaxLength(20);

        builder.Property(p => p.Direccion)
            .HasMaxLength(250);

        builder.Property(p => p.IdTipoIdentificacion)
            .IsRequired();

        builder.Property(p => p.NumeroIdentificacion)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasIndex(p => new { p.IdTipoIdentificacion, p.NumeroIdentificacion })
            .IsUnique();

        builder.HasOne(p => p.TipoIdentificacion)         
             .WithMany(u => u.Personas)        
             .HasForeignKey(p => p.IdTipoIdentificacion) 
             .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.EstadoCivil)
             .WithMany(u => u.Personas)
             .HasForeignKey(p => p.IdEstadoCivil)
             .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Genero)
             .WithMany(u => u.Personas)
             .HasForeignKey(p => p.IdGenero)
             .OnDelete(DeleteBehavior.Restrict);

    }
}
