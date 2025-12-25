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
    public class OcupacionConfiguration : IEntityTypeConfiguration<Ocupacion>
    {
        public void Configure(EntityTypeBuilder<Ocupacion> builder)
        {
            builder.HasKey(g => g.IdOcupacion);
           
            builder.Property(g => g.Nombre)
                   .IsRequired()
                   .HasMaxLength(25);
            builder.Property(g => g.EstaActivo)
                   .IsRequired();
        }
    }
}
