using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EapExampleClient.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public long Salary { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
    }
}
