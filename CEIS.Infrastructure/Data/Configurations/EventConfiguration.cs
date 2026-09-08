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
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.Description)
                .IsRequired()
                .HasColumnType("text");

            builder.Property(e => e.Category)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(e => e.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(e => e.Venue)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.OrganizerId)
                .IsRequired();

            builder.Property(e => e.BannerImageUrl)
                .HasMaxLength(500);

        }
    }
}
