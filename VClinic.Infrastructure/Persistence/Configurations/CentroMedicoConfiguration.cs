using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VClinic.Domain.Entities;

namespace VClinic.Infrastructure.Persistence.Configurations
{
    public class CentroMedicoConfiguration : IEntityTypeConfiguration<CentroMedico>
    {
        public void Configure(EntityTypeBuilder<CentroMedico> builder)
        {
            builder.ToTable("CentrosMedicos");

            builder.HasKey(c => c.IdCentroMedico);

            builder.Property(c => c.Nombre)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Descripcion)
               .IsRequired()
               .HasMaxLength(50);

            builder.Property(c => c.Direccion)
              .IsRequired()
              .HasMaxLength(150);

            builder.Property(c => c.Telefono)
             .IsRequired()
             .HasMaxLength(20);

            builder.Property(c => c.Codigo)
                .IsRequired()
                .HasMaxLength(5);

            builder.Property(c => c.Rnc)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(c => c.FechaFundacion)
                .IsRequired();

            builder.Property(c => c.EstaActivo)
                .IsRequired();

            builder.Property(builder => builder.Tipo)
                .IsRequired();            
        }
    }
}
