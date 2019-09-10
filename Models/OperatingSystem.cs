using System;

namespace computer_check.Models
{
    public class OperatingSystem
    {
        public string OSName { get; set; }
        public string OSVersion { get; set; }
        public string OSBuild { get; set; }
        public DateTime? OSInstallDate { get; set; }
        public string OSManufacturer {get; set;}
        public string OSArchitecture {get;set;}
        public string OSSerialNumber {get;set;}
    }
}
