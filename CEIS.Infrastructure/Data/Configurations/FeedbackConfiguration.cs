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
    public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.UserId)
                .IsRequired();

            builder.Property(f => f.Category)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(30);

            builder.Property(f => f.Rating)
                .IsRequired();

            builder.Property(f => f.Comments)
                .HasMaxLength(1000);

            builder.HasIndex(f => new { f.EventId, f.UserId })
                .IsUnique()
                .HasFilter("[EventId] IS NOT NULL");
        }
    }
}
