using EmployeeApp.Controllers;
using EmployeeApp.Data;
using EmployeeApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmployeeApp.Tests
{
    [TestClass]
    public class EmployeeAppTests
    {
        [TestMethod]
        public void GetAllEmployeesSuccess()
        {
            var controller = new EmployeeController();
            var response = controller.GetAll();
            var result = response as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        }

        [TestMethod]
        public void GetAllEmployeesNotFound()
        {
            var controller = new EmployeeController();
            EmployeeData.EmployeeList.Clear();
            IActionResult actionResult = controller.GetAll();

            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult));
        }

        [DataTestMethod]
        [DataRow(1, 5)]
        public void GetEmployeePagingSuccess(int pageNumber, int pageSize)
        {
            var controller = new EmployeeController();
            var response = controller.Get(pageNumber, pageSize);
            var result = response as OkObjectResult;
            Assert.IsNotNull(response);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        }

        [DataTestMethod]
        [DataRow(0, 0)]
        public void GetEmployeePagingNotFound(int pageNumber, int pageSize)
        {
            var controller = new EmployeeController();
            EmployeeData.EmployeeList.Clear();
            IActionResult actionResult = controller.Get(pageNumber, pageSize);

            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult));
        }

        [DataTestMethod]
        [DataRow(1)]
        public void SearchEmployeeByIdSuccess(int id)
        {
            var controller = new EmployeeController();
            var response = controller.SearchById(id);
            var result = response as OkObjectResult;
            Assert.IsNotNull(response);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        }

        [DataTestMethod]
        [DataRow(1000)]
        [DataRow(0)]
        [DataRow(null)]
        public void SearchEmployeeByIdNotFound(int id)
        {
            var controller = new EmployeeController();
            IActionResult actionResult = controller.SearchById(id);

            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult));
        }

        [DataTestMethod]
        [DataRow("Joel")]
        [DataRow("joel")]
        [DataRow("JOEL")]
        public void SearchEmployeeByNameSuccess(string name)
        {
            var controller = new EmployeeController();
            var response = controller.SearchByName(name);
            var result = response as OkObjectResult;
            Assert.IsNotNull(response);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        }

        [DataTestMethod]
        [DataRow("Dave")]
        [DataRow("")]
        [DataRow(null)]
        [DataRow("DaveDaveDaveDaveDaveDaveDaveDaveDaveDaveDaveDaveDaveDaveDaveDave")]
        public void SearchEmployeeByNameNotFound(string name)
        {
            var controller = new EmployeeController();
            IActionResult actionResult = controller.SearchByName(name);

            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult));
        }

        [DataTestMethod]
        [DataRow(1, "Joel")]
        public void SearchEmployeeByIdAndNameSuccess(int id, string name)
        {
            var controller = new EmployeeController();
            var response = controller.Search(id, name);
            var result = response as OkObjectResult;
            Assert.IsNotNull(response);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        }

        [DataTestMethod]
        [DataRow(2, "Dave")]
        [DataRow(3, "Mike")]
        [DataRow(null, "Jimmy")]
        [DataRow(4, "")]
        [DataRow(4, null)]
        [DataRow(null, null)]
        public void SearchEmployeeByIdAndNameNotFound(int id, string name)
        {
            var controller = new EmployeeController();
            IActionResult actionResult = controller.Search(id, name);

            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult));
        }

        [DataTestMethod]
        [DataRow(2, "Jimmy")]
        public void CreateEmployeeSuccess(int id, string name)
        {
            Employee employee = new Employee();
            employee.EmployeeID = id;
            employee.EmployeeName = name;

            var controller = new EmployeeController();
            var response = controller.Create(employee);
            var result = response as StatusCodeResult;
            Assert.AreEqual(StatusCodes.Status201Created, result.StatusCode);
        }

        [DataTestMethod]
        [DataRow(1, "Jimmy")]
        public void CreateEmployeeBadRequest(int id, string name)
        {
            Employee employee = new Employee();
            employee.EmployeeID = id;
            employee.EmployeeName = name;

            var controller = new EmployeeController();
            var response = controller.Create(employee);
            var result = response as BadRequestObjectResult; 
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.AreEqual(result.Value, "Employee exists. Please enter a unique Employee ID");
        }

        [DataTestMethod]
        [DataRow(1)]
        public void DeleteEmployeeSuccess(int id)
        {
            var controller = new EmployeeController();
            var response = controller.Delete(id);
            var result = response as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        }

        [DataTestMethod]
        [DataRow(2)]
        public void DeleteEmployeeBadRequest(int id)
        {
            var controller = new EmployeeController();
            var response = controller.Delete(id);
            var result = response as BadRequestObjectResult;
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.AreEqual(result.Value, "Employee does not exist");
        }

        [DataTestMethod]
        [DataRow(1, 2, "Jimmy")]
        public void UpdateEmployeeSuccess(int currentId, int updatedId, string name)
        {
            Employee employee = new Employee();
            employee.EmployeeID = updatedId;
            employee.EmployeeName = name;

            var controller = new EmployeeController();
            var response = controller.Update(currentId, employee);
            var result = response as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        }

        [DataTestMethod]
        [DataRow(2, 3, "Jimmy")]
        public void UpdateEmployeeBadRequest(int currentId, int updatedId, string name)
        {
            Employee employee = new Employee();
            employee.EmployeeID = updatedId;
            employee.EmployeeName = name;

            var controller = new EmployeeController();
            var response = controller.Update(currentId, employee);
            var result = response as BadRequestObjectResult;
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.AreEqual(result.Value, "Employee does not exist");
        }
    }
}
