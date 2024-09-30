using AppEmployeesProyect.Models;

namespace AppEmployeesProyect.BusinessLogic
{
	public class EmployeeService
	{
		public decimal CalculateAnnualSalary(Employee employee)
		{
			if (employee == null)
				throw new ArgumentNullException(nameof(employee));

			return (employee.salary * 12);
		}
	}
}
