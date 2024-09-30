using AppEmployeesProyect.BusinessLogic;
using AppEmployeesProyect.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace AppEmployeesProyect.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly EmployeeService _employeeService;

		string baseURL = "https://hub.dummyapis.com/";

        public HomeController(ILogger<HomeController> logger)
		{
            _logger = logger;
			_employeeService = new EmployeeService();
		}

        public async Task<IActionResult> Index(string searchString)
        {
			//Call the API
			List<Employee> employees = new List<Employee>();

			HttpClientHandler handler = new HttpClientHandler()
			{
				// Deshabilitar la validación del certificado SSL
				ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true
			};


			using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				if (!string.IsNullOrEmpty(searchString))
				{
					int idBusqueda = Convert.ToInt32(searchString);
					HttpResponseMessage getData = await client.GetAsync($"employee?noofRecords=1&idStarts={idBusqueda}");

					if (getData.IsSuccessStatusCode)
					{

						string results = await getData.Content.ReadAsStringAsync();
						employees = JsonConvert.DeserializeObject<List<Employee>>(results);

					}
					else if ((int)getData.StatusCode == 429)
					{
						await Task.Delay(ObtenerTiempoDeEspera(getData));
						return await Index(searchString);
					}
					else
					{
						Console.WriteLine("Error calling web Api");
					}
				}
				else
				{
					HttpResponseMessage getData = await client.GetAsync("employee?noofRecords=10&idStarts=1");

					if (getData.IsSuccessStatusCode)
					{
						string results = await getData.Content.ReadAsStringAsync();
						employees = JsonConvert.DeserializeObject<List<Employee>>(results);
					}
					else if ((int)getData.StatusCode == 429)
					{
						await Task.Delay(ObtenerTiempoDeEspera(getData));
						return await Index(searchString);
					}
					else
					{
						Console.WriteLine("Error calling web API for all employees");
					}
				}
			}

			foreach (var employee in employees)
			{
				employee.annualSalary = _employeeService.CalculateAnnualSalary(employee);
			}

			return View(employees);
        }

        //public async Task<IActionResult> Index2() 
        //{
		//	//Call the API
		//	List<Employee> employees = new List<Employee>();

		//	HttpClientHandler handler = new HttpClientHandler()
		//	{
		//		// Deshabilitar la validación del certificado SSL
		//		ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true
		//	};



		//	using (var client = new HttpClient())
		//	{
		//		client.BaseAddress = new Uri(baseURL);
		//		client.DefaultRequestHeaders.Accept.Clear();
		//		client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

		//		//HttpResponseMessage getData = await client.GetAsync($"employee?noofRecords=1&idStarts={id}");
		//		HttpResponseMessage getData = await client.GetAsync("employee?noofRecords=1&idStarts=1");

		//		if (getData.IsSuccessStatusCode)
		//		{

		//			string results = await getData.Content.ReadAsStringAsync();
		//			employees = JsonConvert.DeserializeObject<List<Employee>>(results);

		//		}
		//		else if ((int)getData.StatusCode == 429)
		//		{
		//			await Task.Delay(ObtenerTiempoDeEspera(getData));
		//			return await Index2();
		//		}
		//		else
		//		{
		//			Console.WriteLine("Error calling web Api");
		//		}

		//	}

		//	return View(employees);
		//}



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

		private int ObtenerTiempoDeEspera(HttpResponseMessage response)
		{

			return 60000;
		}
	}
}