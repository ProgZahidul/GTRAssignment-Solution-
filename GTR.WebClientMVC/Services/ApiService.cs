using GTR.WebClientMVC.Models;

namespace GTR.WebClientMVC.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ApiService> _logger;

        public ApiService(HttpClient httpClient, IConfiguration configuration, ILogger<ApiService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration["ApiBaseUrl"]);
            _logger = logger;
        }

 
        public async Task<List<EmployeeViewModel>> GetEmployeesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<EmployeeViewModel>>("api/employees");
        }

        public async Task<EmployeeViewModel> GetEmployeeAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<EmployeeViewModel>($"api/employees/{id}");
        }

        public async Task<(bool success, string error)> CreateEmployeeAsync(EmployeeViewModel employee)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/employees", employee);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return (false, $"API Error: {response.StatusCode} - {errorContent}");
                }
                return (true, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating employee");
                return (false, $"Network Error: {ex.Message}");
            }
        }

        public async Task<(bool success, string error)> UpdateEmployeeAsync(EmployeeViewModel employee)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/employees/{employee.EmployeeId}", employee);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return (false, $"API Error: {response.StatusCode} - {errorContent}");
                }
                return (true, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating employee {employee.EmployeeId}");
                return (false, $"Network Error: {ex.Message}");
            }
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
