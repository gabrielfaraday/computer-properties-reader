using System;
using System.Management;
using computer_check.Models;
using computer_check.Utils;

namespace computer_check.Services
{
    public class OperatingSystemService
    {
        public static void ReadProperties(Computer computer)
        {
            try
            {
                Console.WriteLine("Starting reading Operating system properties...");

                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem"))
                {
                    foreach (var os in searcher.Get())
                    {
                        computer.OperatingSystem.OSName = os["Caption"].AsString();
                        computer.OperatingSystem.OSVersion = os["Version"].AsString();
                        computer.OperatingSystem.OSManufacturer = os["Manufacturer"].AsString();
                        computer.OperatingSystem.OSSerialNumber = os["SerialNumber"].AsString();
                        computer.OperatingSystem.OSBuild = os["BuildNumber"].AsString();
                        computer.OperatingSystem.OSArchitecture = os["OSArchitecture"].AsString();
                        computer.OperatingSystem.OSInstallDate = os["InstallDate"].AsDate();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while reading Operating System properties: {ex.ToString()}");
            }
        }
    }
}