using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VClinic.Domain.Entities.Catalogos;

namespace VClinic.Infrastructure.Persistence.Configurations
{
    public class TipoEquipamentoConfiguration:IEntityTypeConfiguration<TipoEquipamento>
    {
        public void Configure(EntityTypeBuilder<TipoEquipamento> builder)
        {
            builder.ToTable("TipoEquipamento");

            builder.HasKey(c => c.IdTipoEquipamento);

            builder.Property(c => c.Nombre)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(c => c.Descripcion)
              .IsRequired()
              .HasMaxLength(50);

            builder.Property(c => c.EstaActivo)
                .IsRequired();
        }
    }
}
