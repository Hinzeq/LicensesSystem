using System;

namespace ProgramowanieZaawansowaneLicencje.Models

{
    public class License
    {
        public int LicenseId { get; set; }
        public string Key { get; set; }
        public string Type { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
