using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VClinic.Domain.Entities.Catalogos;

namespace VClinic.Infrastructure.Persistence.Configurations
{
    public class DepartamentosConfiguration : IEntityTypeConfiguration<Departamento>
    {
        public void Configure(EntityTypeBuilder<Departamento> builder)
        {
            builder.ToTable("Departamentos");

            builder.HasKey(c => c.IdDepartamento);

            builder.Property(c => c.Nombre)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Codigo)
               .IsRequired()
               .HasMaxLength(15);            

            builder.Property(c => c.EstaActivo)
                .IsRequired();
        }
    }
}
