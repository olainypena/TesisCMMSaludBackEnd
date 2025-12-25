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
    public class TipoIdentificacionConfiguration: IEntityTypeConfiguration<TipoIdentificacion>
    {
        public void Configure(EntityTypeBuilder<TipoIdentificacion> builder)
        {
            builder.ToTable("TipoIdentificacion");

            builder.HasKey(c => c.IdTipoIdentificacion);

            builder.Property(c => c.Nombre)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(c => c.Codigo)
               .IsRequired()
               .HasMaxLength(3);

            builder.Property(c => c.EstaActivo)
                .IsRequired();
        }
    }
}
