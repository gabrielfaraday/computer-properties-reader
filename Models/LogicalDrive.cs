using System;

namespace computer_check.Models
{
    public class LogicalDrive
    {
        public Guid ID { get; set; }
        public string ComputerName { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public string FileSystem { get; set; }
        public string SerialNumber { get; set; }
        public string FreeSpace { get; set; }
        public DateTime? RemovedDate { get; set; }

        public virtual Computer Computer { get; set; }
    }
}
