using System;
using System.Collections.Generic;

namespace computer_check.Models
{
    public class Disk
    {
        public Guid ID { get; set; }
        public string ComputerName { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public string Size { get; set; }
        public string Manufacturer { get; set; }
        public DateTime? RemovedDate { get; set; }
        
        public virtual Computer Computer { get; set; }
    }
}
