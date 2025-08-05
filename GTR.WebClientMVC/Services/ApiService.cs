using GTR.WebClientMVC.Models;

namespace GTR.WebClientMVC.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration["ApiBaseUrl"]);
        }

 
        public async Task<List<EmployeeViewModel>> GetEmployeesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<EmployeeViewModel>>("api/employees");
        }

        public async Task<EmployeeViewModel> GetEmployeeAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<EmployeeViewModel>($"api/employees/{id}");
        }

        public async Task<HttpResponseMessage> CreateEmployeeAsync(EmployeeViewModel employee)
        {
            return await _httpClient.PostAsJsonAsync("api/employees", employee);
        }

        public async Task<HttpResponseMessage> UpdateEmployeeAsync(EmployeeViewModel employee)
        {
            return await _httpClient.PutAsJsonAsync($"api/employees/{employee.EmployeeId}", employee);
        }

        public async Task<HttpResponseMessage> DeleteEmployeeAsync(int id)
        {
            return await _httpClient.DeleteAsync($"api/employees/{id}");
        }

     
        public async Task<List<DepartmentViewModel>> GetDepartmentsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<DepartmentViewModel>>("api/employees/departments");
        }


        public async Task<List<DesignationViewModel>> GetDesignationsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<DesignationViewModel>>("api/employees/designations");
        }
    }
}
