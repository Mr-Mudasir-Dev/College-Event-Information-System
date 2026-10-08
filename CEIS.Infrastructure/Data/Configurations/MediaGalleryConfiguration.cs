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
    public class MediaGalleryConfiguration : IEntityTypeConfiguration<MediaGallery>
    {
        public void Configure(EntityTypeBuilder<MediaGallery> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FileType)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(10);

            builder.Property(x => x.FileUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(m => m.UploadedBy)
                .IsRequired();

            builder.Property(m => m.Caption)
                .HasMaxLength(500);
        }
    }
}
