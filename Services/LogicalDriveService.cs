using System;
using System.Management;
using computer_check.Models;
using computer_check.Utils;

namespace computer_check.Services
{
    public class LogicalDriveService
    {
        public static void ReadProperties(Computer computer)
        {
            try
            {
                Console.WriteLine("Starting reading Logical Drives properties...");

                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk"))
                {
                    foreach (var drive in searcher.Get())
                    {
                        var logicalDrive = new LogicalDrive();

                        logicalDrive.Name = drive["Caption"].AsString();
                        logicalDrive.SerialNumber = drive["VolumeSerialNumber"].AsString();
                        logicalDrive.FileSystem = drive["FileSystem"].AsString();
                        logicalDrive.Size = drive["Size"].AsGigabyte();
                        logicalDrive.FreeSpace = drive["FreeSpace"].AsGigabyte();

                        computer.LogicalDrives.Add(logicalDrive);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while reading Logical Drivers properties: {ex.ToString()}");
            }
        }
    }
}