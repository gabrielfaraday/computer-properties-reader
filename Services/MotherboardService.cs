using System;
using System.Management;
using computer_check.Models;
using computer_check.Utils;

namespace computer_check.Services
{
    public class MotherboardService
    {
        public static void ReadProperties(Computer computer)
        {
            try
            {
                Console.WriteLine("Starting reading Motheboard properties...");

                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard"))
                {
                    foreach (var board in searcher.Get())
                    {
                        computer.Motherboard.MBProductID = board["Product"].AsString();
                        computer.Motherboard.MBVersion = board["Version"].AsString();
                        computer.Motherboard.MBManufacturer = board["Manufacturer"].AsString();
                        computer.Motherboard.MBSerialNumber = board["SerialNumber"].AsString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while reading Motherboard properties: {ex.ToString()}");
            }
        }
    }
}