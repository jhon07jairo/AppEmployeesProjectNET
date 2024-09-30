using Newtonsoft.Json;

namespace AppEmployeesProyect.Models
{
    public class Employee
    {
        public int id { get; set; }

        [JsonProperty("imageUrl")]
        public string imageUrl { get; set; }

        [JsonProperty("firstName")]
		public string firstName { get; set; }

        [JsonProperty("lastName")]
        public string lastName { get; set; }

        [JsonProperty("email")]
        public string email { get; set; }

        [JsonProperty("contactNumber")]
        public string contactNumber { get; set; }

		[JsonProperty("age")]
		public int age { get; set; }

        [JsonProperty("salary")]
        public decimal salary { get; set; }

        [JsonProperty("address")]
        public string address { get; set; }

		public decimal annualSalary { get; set; }
	}
}
