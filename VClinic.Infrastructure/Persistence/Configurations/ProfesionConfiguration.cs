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
    public class ProfesionConfiguration: IEntityTypeConfiguration<Profesion>
    {
        public void Configure(EntityTypeBuilder<Profesion> builder)
        {
            builder.ToTable("Profesion");

            builder.HasKey(c => c.IdProfesion);

            builder.Property(c => c.Nombre)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.EstaActivo)
                .IsRequired();
        }
    }
}
