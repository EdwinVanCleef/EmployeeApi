using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApp.Models
{
    public class Employee
    {
        [Range(1, 999)]
        [Required(ErrorMessage = "Employee ID is required")]
        public int EmployeeID { get; set; }

        [StringLength(60, MinimumLength = 2)]
        [Required(ErrorMessage = "Employee Name is required")]
        public string EmployeeName { get; set; }
    }
}
