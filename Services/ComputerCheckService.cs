using System;
using computer_check.Data.Repositories;
using computer_check.Models;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace computer_check.Services
{
    public class ComputerCheckService
    {
        public Computer CheckUpProperties()
        {
            try
            {
                var computer = new Computer();

                ComputerService.ReadProperties(computer);
                BiosService.ReadProperties(computer);
                DiskService.ReadProperties(computer);
                LogicalDriveService.ReadProperties(computer);
                MotherboardService.ReadProperties(computer);
                NetworkAdapterService.ReadProperties(computer);
                OperatingSystemService.ReadProperties(computer);
                ProcessorService.ReadProperties(computer);
                SoftwareService.ReadProperties(computer);
                WindowsProductKeyService.ReadKey(computer);

                JsonFileService.WriteFile(computer);

                return computer;         
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while checking up propeties: {ex.ToString()}");
                return null;
            }
        }

        public void SaveComputerToDB(Computer computer)
        {
            computer.LastUsage = DateTime.Now;
            
            var repository = new ComputerRepository();
            var computerDB = repository.FindByName(computer.Name);

            if (computerDB == null)
                AddComputer(computer, repository);
            else
                UpdateComputer(computer, repository, computerDB);
        }

        private static void AddComputer(Computer computer, ComputerRepository repository)
        {
            repository.Add(computer);
        }

        private static void UpdateComputer(Computer computer, ComputerRepository repository, Computer computerDB)
        {
            computerDB.LastUsage = computer.LastUsage;
            computerDB.Chassis = computer.Chassis;
            computerDB.DecodedWindowsProductKey = computer.DecodedWindowsProductKey;
            computerDB.Manufacturer = computer.Manufacturer;
            computerDB.Model = computer.Model;
            computerDB.Motherboard = computer.Motherboard;
            computerDB.Name = computer.Name;
            computerDB.PhysicalMemory = computer.PhysicalMemory;
            computerDB.SerialNumber = computer.SerialNumber;
            computerDB.UndecodedWindowsProductKey = computer.UndecodedWindowsProductKey;
            computerDB.UserName = computer.UserName;

            computerDB.Bios.BiosDate = computer.Bios.BiosDate;
            computerDB.Bios.BiosManufacturer = computer.Bios.BiosManufacturer;
            computerDB.Bios.BiosVersion = computer.Bios.BiosVersion;

            computerDB.OperatingSystem.OSArchitecture = computer.OperatingSystem.OSArchitecture;
            computerDB.OperatingSystem.OSBuild = computer.OperatingSystem.OSBuild;
            computerDB.OperatingSystem.OSInstallDate = computer.OperatingSystem.OSInstallDate;
            computerDB.OperatingSystem.OSManufacturer = computer.OperatingSystem.OSManufacturer;
            computerDB.OperatingSystem.OSName = computer.OperatingSystem.OSName;
            computerDB.OperatingSystem.OSSerialNumber = computer.OperatingSystem.OSSerialNumber;
            computerDB.OperatingSystem.OSVersion = computer.OperatingSystem.OSVersion;

            computerDB.Processor.ProcessorArchitecture = computer.Processor.ProcessorArchitecture;
            computerDB.Processor.ProcessorManufacturer = computer.Processor.ProcessorManufacturer;
            computerDB.Processor.ProcessorName = computer.Processor.ProcessorName;
            computerDB.Processor.ProcessorNumberOfCores = computer.Processor.ProcessorNumberOfCores;
            computerDB.Processor.ProcessorNumberOfLogicalProcessors = computer.Processor.ProcessorNumberOfLogicalProcessors;

            computerDB.Disks
                .Where(x => !computer.Disks.Any(y => y.Name == x.Name))
                .ToList()
                .ForEach(x => x.RemovedDate = DateTime.Now);

            computer.Disks.ForEach(d =>
            {
                if (computerDB.Disks.Any(x => x.Name == d.Name))
                {
                    var existingDisk = computerDB.Disks.Single(x => x.Name == d.Name);

                    existingDisk.Manufacturer = d.Manufacturer;
                    existingDisk.SerialNumber = d.SerialNumber;
                    existingDisk.Size = d.Size;
                    existingDisk.RemovedDate = null;
                }
                else
                {
                    computerDB.Disks.Add(d);
                }
            });

            computerDB.LogicalDrives
                .Where(x => !computer.LogicalDrives.Any(y => y.Name == x.Name))
                .ToList()
                .ForEach(x => x.RemovedDate = DateTime.Now);

            computer.LogicalDrives.ForEach(ld =>
            {
                if (computerDB.LogicalDrives.Any(x => x.Name == ld.Name))
                {
                    var existingDrive = computerDB.LogicalDrives.Single(x => x.Name == ld.Name);

                    existingDrive.Size = ld.Size;
                    existingDrive.FileSystem = ld.FileSystem;
                    existingDrive.SerialNumber = ld.SerialNumber;
                    existingDrive.FreeSpace = ld.FreeSpace;
                    existingDrive.RemovedDate = null;
                }
                else
                {
                    computerDB.LogicalDrives.Add(ld);
                }
            });

            computerDB.NetworkAdapters
                .Where(x => !computer.NetworkAdapters.Any(y => y.Name == x.Name))
                .ToList()
                .ForEach(x => x.RemovedDate = DateTime.Now);

            computer.NetworkAdapters.ForEach(na =>
            {
                if (computerDB.NetworkAdapters.Any(x => x.Name == na.Name))
                {
                    var existingAdapter = computerDB.NetworkAdapters.Single(x => x.Name == na.Name);

                    existingAdapter.MACAddress = na.MACAddress;
                    existingAdapter.Manufacturer = na.Manufacturer;
                    existingAdapter.NetConnectionID = na.NetConnectionID;
                    existingAdapter.NetEnabled = na.NetEnabled;
                    existingAdapter.RemovedDate = null;
                }
                else
                {
                    computerDB.NetworkAdapters.Add(na);
                }
            });

            computerDB.Softwares
                .Where(x => !computer.Softwares.Any(y => y.Name == x.Name && y.Publisher == x.Publisher && y.Version == x.Version && y.Type == x.Type))
                .ToList()
                .ForEach(x => x.RemovedDate = DateTime.Now);

            computer.Softwares.ForEach(s =>
            {
                if (computerDB.Softwares.Any(x => s.Name == x.Name && s.Publisher == x.Publisher && s.Version == x.Version && s.Type == x.Type))
                {
                    var existingSoftware = computerDB.Softwares.Single(x => s.Name == x.Name && s.Publisher == x.Publisher && s.Version == x.Version && s.Type == x.Type);

                    existingSoftware.InstallDate = s.InstallDate;
                    existingSoftware.RemovedDate = null;
                }
                else
                {
                    computerDB.Softwares.Add(s);
                }
            });

            repository.Update(computerDB);
        }
    }
}