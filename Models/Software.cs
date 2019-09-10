using System;

namespace computer_check.Models
{
    public class Software
    {
        public Guid ID { get; set; }
        public string ComputerName { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string Publisher { get; set; }
        public DateTime? InstallDate { get; set; }
        public DateTime? RemovedDate { get; set; }
        public string Type { get; set; }
        public virtual Computer Computer { get; set; }
    }
}
