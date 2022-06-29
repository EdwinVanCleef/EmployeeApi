using EmployeeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApp.Data
{
    public class EmployeeData
    {
        public static List<Employee> EmployeeList = new List<Employee>()
        {
            new Employee() {EmployeeID = 1, EmployeeName = "Joel" },
        };
    }
}
