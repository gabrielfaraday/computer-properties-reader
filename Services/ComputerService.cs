using System;
using System.Management;
using computer_check.Models;
using computer_check.Utils;

namespace computer_check.Services
{
    public class ComputerService
    {
        public static void ReadProperties(Computer computer)
        {
            try
            {
                Console.WriteLine("Starting reading Computer properties...");

                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem"))
                {
                    foreach (var comp in searcher.Get())
                    {
                        computer.Name = comp["Caption"].AsString();
                        computer.Manufacturer = comp["Manufacturer"].AsString();
                        computer.PhysicalMemory = comp["TotalPhysicalMemory"].AsGigabyte();
                        computer.UserName = comp["UserName"].AsUser();
                        computer.Model = comp["Model"].AsString();
                        computer.Processor.ProcessorArchitecture = comp["SystemType"].AsString();
                        computer.Chassis = comp["ChassisSKUiNumber"].AsString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while reading computer properties: {ex.ToString()}");
            }
        }
    }
}