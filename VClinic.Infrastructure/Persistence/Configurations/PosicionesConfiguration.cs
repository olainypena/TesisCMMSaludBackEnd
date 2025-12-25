using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VClinic.Domain.Entities;
using VClinic.Domain.Entities.Catalogos;

namespace VClinic.Infrastructure.Persistence.Configurations
{
    public class PosicionesConfiguration: IEntityTypeConfiguration<Posiciones>
    {
        public void Configure(EntityTypeBuilder<Posiciones> builder)
        {
            builder.HasKey(e => e.IdPosicion);

            builder.Property(e => e.IdPosicion)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(e => e.Nivel)
                .IsRequired();

            builder.Property(e => e.EstaActivo)
                .IsRequired()
                .HasDefaultValue(true);
        }

    }
}
