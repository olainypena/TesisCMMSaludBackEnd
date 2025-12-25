using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VClinic.Domain.Entities;

namespace VClinic.Infrastructure.Persistence.Configurations
{
    public class EquipamentoConfiguration : IEntityTypeConfiguration<Equipamento>
    {
        public void Configure(EntityTypeBuilder<Equipamento> builder)
        {
            builder.ToTable("Equipamento");

            builder.HasKey(c => c.IdEquipamento);

            builder.Property(c => c.Nombre)
                .IsRequired()
                .HasMaxLength(35);

            builder.Property(c => c.Descripcion)
               .IsRequired()
               .HasMaxLength(50);

            builder.Property(c => c.IdTipoEquipo)
               .IsRequired();

            builder.Property(c => c.EstaActivo)
                .IsRequired();
        }
    }
}
