using System;
using System.Management;
using computer_check.Models;
using computer_check.Utils;

namespace computer_check.Services
{
    public class DiskService
    {
        public static void ReadProperties(Computer computer)
        {
            try
            {
                Console.WriteLine("Starting reading Disks properties...");

                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive"))
                {
                    foreach (var diskDrive in searcher.Get())
                    {
                        var disk = new Disk();

                        disk.Name = diskDrive["Caption"].AsString();
                        disk.SerialNumber = diskDrive["SerialNumber"].AsString();
                        disk.Manufacturer = diskDrive["Manufacturer"].AsString();
                        disk.Size = diskDrive["Size"].AsGigabyte();

                        computer.Disks.Add(disk);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while reading Disk properties: {ex.ToString()}");
            }

        }
    }
}