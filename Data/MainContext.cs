using computer_check.Data.Mappings;
using computer_check.Models;
using Microsoft.EntityFrameworkCore;

namespace computer_check.Data
{
    public class MainContext : DbContext
    {
        public DbSet<Computer> Computers { get; set; }
        public DbSet<Disk> Disks { get; set; }
        public DbSet<LogicalDrive> LogicalDrives { get; set; }
        public DbSet<NetworkAdapter> NetworkAdapters { get; set; }
        public DbSet<Software> Softwares { get; set; }

        public MainContext(DbContextOptions<MainContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ComputerMapping());
            modelBuilder.ApplyConfiguration(new DiskMapping());
            modelBuilder.ApplyConfiguration(new LogicalDriveMapping());
            modelBuilder.ApplyConfiguration(new NetworkAdapterMapping());
            modelBuilder.ApplyConfiguration(new SoftwareMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}