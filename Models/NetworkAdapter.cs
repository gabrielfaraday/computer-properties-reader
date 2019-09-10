
using System;

namespace computer_check.Models
{
    public class NetworkAdapter
    {
        public Guid ID { get; set; }
        public string ComputerName { get; set; }
        public string Name { get; set; }
        public string MACAddress { get; set; }
        public string Manufacturer { get; set; }
        public string NetConnectionID { get; set; }
        public bool NetEnabled { get; set; }
        public DateTime? RemovedDate { get; set; }
        
        public virtual Computer Computer { get; set; }
    }
}
