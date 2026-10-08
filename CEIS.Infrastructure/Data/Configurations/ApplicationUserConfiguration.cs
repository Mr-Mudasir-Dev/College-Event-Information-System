using CEIS.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Infrastructure.Data.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.FullName)
            .IsRequired()
            .HasMaxLength(100);

            builder.Property(u => u.Department)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.EnrollmentNo)
                .HasMaxLength(50);

            // Unique constraint — lekin NULL values allow honi chahiye (Organizer/Admin ke liye)
            builder.HasIndex(u => u.EnrollmentNo)
                .IsUnique()
                .HasFilter("[EnrollmentNo] IS NOT NULL");
        }
    }
}
