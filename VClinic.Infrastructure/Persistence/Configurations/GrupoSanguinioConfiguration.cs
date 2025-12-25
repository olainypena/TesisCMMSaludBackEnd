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
    public class GrupoSanguinioConfiguration : IEntityTypeConfiguration<GrupoSanguinio>
    {
        public void Configure(EntityTypeBuilder<GrupoSanguinio> builder)
        {
            builder.ToTable("GrupoSangre");

            builder.HasKey(c => c.IdGrupoSangre);

            builder.Property(c => c.TipoSangre)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(c => c.GrupoABO)
                .IsRequired()
                .HasMaxLength(2);

            builder.Property(c => c.FactorRH)
               .IsRequired()
               .HasMaxLength(10);

            builder.Property(c => c.EstaActivo)
                .IsRequired();

        }
    }
}
