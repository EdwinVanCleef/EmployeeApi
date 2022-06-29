using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeApp.Data;
using EmployeeApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApp.Controllers
{
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private const string _employeeNotFoundMessage = "Employee not found";
        private const string _employeeNotExistMessage = "Employee does not exist";

        [HttpGet("employee")]
        public IActionResult Get(int pageNumber, int pageSize)
        {
            if (EmployeeData.EmployeeList.Count == 0)
                return NotFound("No data available");

            var employeeList = EmployeeData.EmployeeList
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return Ok(employeeList);
        }

        [HttpGet("employee/[action]")]
        public IActionResult GetAll()
        {
            if (EmployeeData.EmployeeList.Count == 0)
                return NotFound("No data available");

            return Ok(EmployeeData.EmployeeList);
        }

        [HttpGet("employee/[action]")]
        public IActionResult SearchById(int id)
        {
            var employeeList = EmployeeData.EmployeeList.Where(
                x => x.EmployeeID.Equals(id));

            if (employeeList.Count() == 0)
                return NotFound(_employeeNotFoundMessage);

            return Ok(employeeList);
        }

        [HttpGet("employee/[action]")]
        public IActionResult SearchByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return NotFound("Employee name is required");

            var employeeList = EmployeeData.EmployeeList.Where(
                x => x.EmployeeName.ToUpper().Equals(name.ToUpper()));

            if (employeeList.Count() == 0)
                return NotFound(_employeeNotFoundMessage);

            return Ok(employeeList);
        }

        [HttpGet("employee/[action]")]
        public IActionResult Search(int id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return NotFound("Employee name is required");

            var employeeList = EmployeeData.EmployeeList.Where(
                x => x.EmployeeName.ToUpper().Equals(name.ToUpper())
                && x.EmployeeID.Equals(id));

            if (employeeList.Count() == 0)
                return NotFound(_employeeNotFoundMessage);

            return Ok(employeeList);
        }

        [HttpPost("employee/[action]")]
        public IActionResult Create([FromBody]Employee employee)
        {
            if (EmployeeData.EmployeeList.Any(x => x.EmployeeID.Equals(employee.EmployeeID)))
                return BadRequest("Employee exists. Please enter a unique Employee ID");

            EmployeeData.EmployeeList.Add(employee);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost("employee/[action]/{id}")]
        public IActionResult Delete(int id)
        {
            if (!EmployeeData.EmployeeList.Any(x => x.EmployeeID.Equals(id)))
                return BadRequest(_employeeNotExistMessage);

            EmployeeData.EmployeeList.RemoveAt(id - 1);
            return Ok("Employee record deleted successfully");
        }

        [HttpPost("employee/Update/{id}")]
        public IActionResult Update(int id, [FromBody]Employee employee)
        {
            if (!EmployeeData.EmployeeList.Any(x => x.EmployeeID.Equals(id)))
                return BadRequest(_employeeNotExistMessage);

            EmployeeData.EmployeeList[id - 1] = employee;
            return Ok("Employee record updated successfully");
        }
    }
}