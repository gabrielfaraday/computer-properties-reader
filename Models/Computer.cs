using System;
using System.Collections.Generic;

namespace computer_check.Models
{
    /// https://docs.microsoft.com/en-us/previous-versions//aa394084(v=vs.85)
    /// https://docs.microsoft.com/pt-br/windows/desktop/WmiSdk/wmi-tasks-for-scripts-and-applications
    public class Computer
    {
        public Computer()
        {
            OperatingSystem = new OperatingSystem();
            Bios = new Bios();
            Motherboard = new Motherboard();
            Processor = new Processor();
            Disks = new List<Disk>();
            LogicalDrives = new List<LogicalDrive>();
            NetworkAdapters = new List<NetworkAdapter>();
            Softwares = new List<Software>();
        }

        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public string Model { get; set; }
        public string UndecodedWindowsProductKey { get; set; }
        public string DecodedWindowsProductKey { get; set; }
        public string Chassis { get; set; }
        public string PhysicalMemory { get; set; }
        public string Manufacturer {get;set;}
        public string UserName {get; set;}
        public DateTime LastUsage { get; set; }

        public virtual OperatingSystem  OperatingSystem { get; set; }
        public virtual Bios Bios { get; set; }
        public virtual Motherboard Motherboard { get; set; }
        public virtual Processor Processor { get; set; }
        public virtual List<Disk> Disks { get; set; }
        public virtual List<LogicalDrive> LogicalDrives { get; set; }
        public virtual List<NetworkAdapter> NetworkAdapters { get; set; }
        public virtual List<Software> Softwares { get; set; }
    }
}
