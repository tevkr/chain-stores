using System.Text;
using System.Text.Json;
using StoreService.Dtos;

namespace StoreService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }


        public async Task SendStoreToEmployee(StoreReadDto store)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(store),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync($"{_configuration["EmployeeService"]}", httpContent);

            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to EmployeeService was OK!");
            }
            else
            {
                Console.WriteLine("--> Sync POST to EmployeeService was NOT OK!");
            }
        }

        public async Task SendStoreToProduct(StoreReadDto store)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(store),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync($"{_configuration["ProductService"]}", httpContent);

            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to ProductService was OK!");
            }
            else
            {
                Console.WriteLine("--> Sync POST to ProductService was NOT OK!");
            }
        }
    }
}