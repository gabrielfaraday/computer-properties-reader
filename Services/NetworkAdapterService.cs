using System;
using System.Management;
using computer_check.Models;
using computer_check.Utils;

namespace computer_check.Services
{
    public class NetworkAdapterService
    {
        public static void ReadProperties(Computer computer)
        {
            try
            {
                Console.WriteLine("Starting reading Network Adapters properties...");
                
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapter"))
                {
                    foreach (var adapter in searcher.Get())
                    {
                        if (adapter["NetConnectionID"].AsString() != null)
                        {
                            var networkAdapter = new NetworkAdapter();

                            networkAdapter.Name = adapter["Name"].AsString();
                            networkAdapter.MACAddress = adapter["MACAddress"].AsString();
                            networkAdapter.Manufacturer = adapter["Manufacturer"].AsString();
                            networkAdapter.NetConnectionID = adapter["NetConnectionID"].AsString();
                            networkAdapter.NetEnabled = adapter["NetEnabled"].AsBoolean();

                            computer.NetworkAdapters.Add(networkAdapter);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while reading Network Adapters properties: {ex.ToString()}");
            }
        }
    }
}