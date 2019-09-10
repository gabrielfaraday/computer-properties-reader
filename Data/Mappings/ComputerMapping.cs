using computer_check.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace computer_check.Data.Mappings
{
    public class ComputerMapping : IEntityTypeConfiguration<Computer>
    {
        public void Configure(EntityTypeBuilder<Computer> builder)
        {
            builder.HasKey(c => c.Name);
            builder.Property(c => c.Name)
               .HasMaxLength(100);

            builder.Property(c => c.SerialNumber)
               .HasMaxLength(1000);

            builder.Property(c => c.Model)
               .HasMaxLength(1000);

            builder.Property(c => c.UndecodedWindowsProductKey)
                .HasMaxLength(1000);

            builder.Property(c => c.DecodedWindowsProductKey)
                .HasMaxLength(1000);

            builder.Property(c => c.Chassis)
                .HasMaxLength(1000);

            builder.Property(c => c.PhysicalMemory)
                .HasMaxLength(1000);

            builder.Property(c => c.Manufacturer)
                .HasMaxLength(1000);

            builder.Property(c => c.UserName)
                .HasMaxLength(1000);

            builder.OwnsOne(
                o => o.OperatingSystem,
                os =>
                {
                    os.Property(p => p.OSBuild).HasColumnName("OS_Build");
                    os.Property(p => p.OSInstallDate).HasColumnName("OS_InstallDate");
                    os.Property(p => p.OSManufacturer).HasColumnName("OS_Manufacturer");
                    os.Property(p => p.OSName).HasColumnName("OS_Name");
                    os.Property(p => p.OSArchitecture).HasColumnName("OS_Architecture");
                    os.Property(p => p.OSSerialNumber).HasColumnName("OS_SerialNumber");
                    os.Property(p => p.OSVersion).HasColumnName("OS_Version");
                });

            builder.OwnsOne(
                o => o.Bios,
                b =>
                {
                    b.Property(p => p.BiosDate).HasColumnName("Bios_Date");
                    b.Property(p => p.BiosManufacturer).HasColumnName("Bios_Manufacturer");
                    b.Property(p => p.BiosVersion).HasColumnName("Bios_Version");
                });

            builder.OwnsOne(
                o => o.Motherboard,
                m =>
                {
                    m.Property(p => p.MBProductID).HasColumnName("Motherboard_ProductID");
                    m.Property(p => p.MBManufacturer).HasColumnName("Motherboard_Manufacturer");
                    m.Property(p => p.MBVersion).HasColumnName("Motherboard_Version");
                    m.Property(p => p.MBSerialNumber).HasColumnName("Motherboard_SerialNumber");
                });

            builder.OwnsOne(
                o => o.Processor,
                pr =>
                {
                    pr.Property(p => p.ProcessorName).HasColumnName("Processor_Name");
                    pr.Property(p => p.ProcessorManufacturer).HasColumnName("Processor_Manufacturer");
                    pr.Property(p => p.ProcessorArchitecture).HasColumnName("Processor_Architecture");
                    pr.Property(p => p.ProcessorNumberOfCores).HasColumnName("Processor_NumberOfCores");
                    pr.Property(p => p.ProcessorNumberOfLogicalProcessors).HasColumnName("Processor_NumberOfLogicalProcessors");

                });

            builder.ToTable("Computers");
        }
    }
}