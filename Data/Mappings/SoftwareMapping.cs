using computer_check.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace computer_check.Data.Mappings
{
    public class SoftwareMapping : IEntityTypeConfiguration<Software>
    {
        public void Configure(EntityTypeBuilder<Software> builder)
        {
            builder.HasKey(c => c.ID);

            builder.Property(c => c.Name)
               .HasMaxLength(1000);

            builder.Property(c => c.Version)
               .HasMaxLength(1000);

            builder.Property(c => c.Publisher)
                .HasMaxLength(1000);

            builder.Property(c => c.InstallDate);

            builder.Property(c => c.Type)
                .HasMaxLength(1000);

            builder.Property(c => c.ComputerName)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasOne(c => c.Computer)
                .WithMany(e => e.Softwares)
                .HasForeignKey(e => e.ComputerName)
                .IsRequired();

            builder.ToTable("Softwares");
        }
    }
}