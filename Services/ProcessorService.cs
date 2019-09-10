using System;
using System.Management;
using computer_check.Models;
using computer_check.Utils;

namespace computer_check.Services
{
    public class ProcessorService
    {
        public static void ReadProperties(Computer computer)
        {
            try
            {
                Console.WriteLine("Starting reading Processor properties...");

                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor"))
                {
                    foreach (var processor in searcher.Get())
                    {
                        computer.Processor.ProcessorName = processor["Name"].AsString();
                        computer.Processor.ProcessorManufacturer = processor["Manufacturer"].AsString();
                        computer.Processor.ProcessorNumberOfCores = processor["NumberOfCores"].AsString();
                        computer.Processor.ProcessorNumberOfLogicalProcessors = processor["NumberOfLogicalProcessors"].AsString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while reading Processor properties: {ex.ToString()}");
            }
        }
    }
}