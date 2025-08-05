using GTR.WebAPI.Models;
using System;

namespace GTR.WebAPI.Repositories
{

    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeInfo>> GetAllEmployeesAsync();
        Task<EmployeeInfo> GetEmployeeByIdAsync(int id);
        Task<EmployeeInfo> AddEmployeeAsync(EmployeeInfo employee);
        Task UpdateEmployeeAsync(EmployeeInfo employee);
        Task DeleteEmployeeAsync(int id);
    }
}
