using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppEmployeesProyect.Models;
using AppEmployeesProyect.BusinessLogic;
using NUnit.Framework;

namespace AppEmployeesProyect.Tests
{
	public class EmployeeServiceTests
	{
		private EmployeeService _employeeService;

		[SetUp]
		public void Setup()
		{
			_employeeService = new EmployeeService();
		}

		[Test]
		public void CalculateAnnualSalary_ValidEmployee_ReturnsAnnualSalary()
		{
			// Arrange
			var employee = new Employee
			{
				salary = 1000m // Ejemplo de salario 
			};

			// Act
			var result = _employeeService.CalculateAnnualSalary(employee);

			// Assert
			Assert.AreEqual(12000m, result); // 1000 * 12 = 12000
		}

		[Test]
		public void CalculateAnnualSalary_NullEmployee_ThrowsArgumentNullException()
		{
			// Arrange
			Employee employee = null;

			// Act & Assert
			Assert.Throws<ArgumentNullException>(() => _employeeService.CalculateAnnualSalary(employee));
		}
	}
}
