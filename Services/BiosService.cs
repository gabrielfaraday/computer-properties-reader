using System;
using System.Management;
using computer_check.Models;
using computer_check.Utils;

namespace computer_check.Services
{
    public class BiosService
    {
        public static void ReadProperties(Computer computer)
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS"))
                {
                    Console.WriteLine("Starting reading BIOS properties...");

                    foreach (var bios in searcher.Get())
                    {
                        computer.SerialNumber = bios["SerialNumber"].AsString();
                        computer.Bios.BiosDate = bios["ReleaseDate"].AsDate();
                        computer.Bios.BiosManufacturer = bios["Manufacturer"].AsString();
                        computer.Bios.BiosVersion = bios["SMBIOSBIOSVersion"].AsString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while reading BIOS properties: {ex.ToString()}");
            }
        }
    }
}