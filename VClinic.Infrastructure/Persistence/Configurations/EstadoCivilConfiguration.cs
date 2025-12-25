using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VClinic.Domain.Entities.Catalogos;

namespace VClinic.Infrastructure.Persistence.Configurations
{
    public class EstadoCivilConfiguration : IEntityTypeConfiguration<EstadoCivil>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<EstadoCivil> builder)
        {
            builder.ToTable("EstadoCivil");
            builder.HasKey(c => c.IdEstadoCivil);
            builder.Property(c => c.Nombre)
                .IsRequired()
                .HasMaxLength(20);           
            builder.Property(c => c.EstaActivo)
                .IsRequired();
        }
    }
}
