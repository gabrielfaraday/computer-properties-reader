using computer_check.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace computer_check.Data.Mappings
{
    public class NetworkAdapterMapping : IEntityTypeConfiguration<NetworkAdapter>
    {
        public void Configure(EntityTypeBuilder<NetworkAdapter> builder)
        {
            builder.HasKey(c => c.ID);

            builder.Property(c => c.Name)
               .HasMaxLength(1000);

            builder.Property(c => c.MACAddress)
               .HasMaxLength(1000);

            builder.Property(c => c.Manufacturer)
                .HasMaxLength(1000);

            builder.Property(c => c.NetConnectionID)
                .HasMaxLength(1000);

            builder.Property(c => c.NetEnabled)
                .IsRequired();

            builder.Property(c => c.ComputerName)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasOne(c => c.Computer)
                .WithMany(e => e.NetworkAdapters)
                .HasForeignKey(e => e.ComputerName)
                .IsRequired();

            builder.ToTable("NetworkAdapters");
        }
    }
}