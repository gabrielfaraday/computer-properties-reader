using computer_check.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace computer_check.Data.Mappings
{
    public class LogicalDriveMapping : IEntityTypeConfiguration<LogicalDrive>
    {
        public void Configure(EntityTypeBuilder<LogicalDrive> builder)
        {
            builder.HasKey(c => c.ID);

            builder.Property(c => c.Name)
               .HasMaxLength(1000);

            builder.Property(c => c.Size)
               .HasMaxLength(1000);

            builder.Property(c => c.FileSystem)
                .HasMaxLength(1000);

            builder.Property(c => c.SerialNumber)
                .HasMaxLength(1000);

            builder.Property(c => c.FreeSpace)
                .HasMaxLength(1000);

            builder.Property(c => c.ComputerName)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasOne(c => c.Computer)
                .WithMany(e => e.LogicalDrives)
                .HasForeignKey(e => e.ComputerName)
                .IsRequired();

            builder.ToTable("LogicalDrives");
        }
    }
}