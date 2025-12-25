using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VClinic.Domain.Entities.Catalogos;

namespace VClinic.Infrastructure.Persistence.Configurations
{
    public class GeneroConfiguration: IEntityTypeConfiguration<Genero>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Genero> builder)
        {
            builder.HasKey(g => g.IdGenero);
            builder.Property(g => g.Codigo)
                   .IsRequired()
                   .HasMaxLength(10);
            builder.Property(g => g.Nombre)
                   .IsRequired()
                   .HasMaxLength(20);
            builder.Property(g => g.EstaActivo)
                   .IsRequired();
        }
    }
}
