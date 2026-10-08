using CEIS.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Infrastructure.Data.Configurations
{
    public class CertificateConfiguration : IEntityTypeConfiguration<Certificate>
    {
        public void Configure(EntityTypeBuilder<Certificate> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.StudentId)
                .IsRequired();

            builder.Property(c => c.CertificateUrl).IsRequired().HasMaxLength(500);

            builder.HasIndex(x => new { x.EventId, x.StudentId })
                .IsUnique();
        }
    }
}
