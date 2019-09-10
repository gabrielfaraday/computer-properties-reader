using System;
using System.Linq;
using computer_check.Models;
using computer_check.Utils;
using Microsoft.Win32;

namespace computer_check.Services
{
    public class SoftwareService
    {
        public static void ReadProperties(Computer computer)
        {
            try
            {
                var registry_key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
                ReadProperties(computer, "x86", registry_key);

                if (computer.OperatingSystem.OSArchitecture != null &&
                    computer.OperatingSystem.OSArchitecture.Contains("64"))
                {
                    registry_key = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";
                    ReadProperties(computer, "x64", registry_key);
                }

                computer.Softwares = computer.Softwares.Distinct().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while reading Softwares properties: {ex.ToString()}");
            }
        }

        private static void ReadProperties(Computer computer, string type, string registry_key)
        {
            Console.WriteLine($"Starting reading Softwares ({type}) properties...");

            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key))
            {
                foreach (string keyNames in key.GetSubKeyNames())
                {
                    using (RegistryKey subkey = key.OpenSubKey(keyNames))
                    {
                        if (subkey.GetValue("DisplayName").AsString() != null)
                        {
                            var software = new Software();

                            software.Name = subkey.GetValue("DisplayName").AsString();
                            software.Version = subkey.GetValue("Version").AsString();
                            software.Publisher = subkey.GetValue("Publisher").AsString();
                            software.InstallDate = subkey.GetValue("InstallDate").AsDate();
                            software.Type = type;

                            computer.Softwares.Add(software);
                        }
                    }
                }
            }
        }


    }
}