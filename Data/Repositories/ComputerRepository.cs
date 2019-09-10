using System;
using System.Linq;
using computer_check.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

namespace computer_check.Data.Repositories
{
    public class ComputerRepository
    {
        protected MainContext Db;

        public ComputerRepository()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MainContext>();
            optionsBuilder.UseMySql("Server=localhost;User Id=root;Password=root;Database=computer_check");
            Db = new MainContext(optionsBuilder.Options); 

            // Db.Computers = Db.Set<Computer>();
            // Db.Disks = Db.Set<Disk>();
            // Db.LogicalDrives = Db.Set<LogicalDrive>();
            // Db.NetworkAdapters = Db.Set<NetworkAdapter>();
            // Db.Softwares = Db.Set<Software>();
        }

        public void Add(Computer computer)
        {
            Db.Computers.Add(computer);
            Db.SaveChanges();
        }

        public void Update(Computer computer)
        {
            Db.Computers.Update(computer);
            Db.SaveChanges();
        }

        public Computer FindByName(string name)
        {
            return Db.Computers
                .AsNoTracking()
                .Include(x => x.Disks)
                .Include(x => x.NetworkAdapters)
                .Include(x => x.Softwares)
                .Include(x => x.LogicalDrives)
                .FirstOrDefault(t => t.Name == name);
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}