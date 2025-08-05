using GTR.WebAPI.Dtos;
using GTR.WebAPI.Models;
using GTR.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GTR.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();
            var employeeDtos = employees.Select(e => new EmployeeDto
            {
                EmployeeId = e.EmployeeId,
                EmployeeName = e.EmployeeName,
                Address = e.Address,
                JoinDate = e.JoinDate,
                Salary = e.Salary,
                IsActive = e.IsActive,
                DepartmentId = e.DepartmentId,
                DesignationId = e.DesignationId
            });

            return Ok(employeeDtos);
        }

     
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            var employeeDto = new EmployeeDto
            {
                EmployeeId = employee.EmployeeId,
                EmployeeName = employee.EmployeeName,
                Address = employee.Address,
                JoinDate = employee.JoinDate,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                DepartmentId = employee.DepartmentId,
                DesignationId = employee.DesignationId
            };

            return employeeDto;
        }

        
        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> PostEmployee(EmployeeCreateDto employeeCreateDto)
        {
            var employee = new EmployeeInfo
            {
                EmployeeName = employeeCreateDto.EmployeeName,
                Address = employeeCreateDto.Address,
                JoinDate = employeeCreateDto.JoinDate,
                Salary = employeeCreateDto.Salary,
                IsActive = employeeCreateDto.IsActive,
                DepartmentId = employeeCreateDto.DepartmentId,
                DesignationId = employeeCreateDto.DesignationId
            };

            await _employeeRepository.AddEmployeeAsync(employee);

            var employeeDto = new EmployeeDto
            {
                EmployeeId = employee.EmployeeId,
                EmployeeName = employee.EmployeeName,
                Address = employee.Address,
                JoinDate = employee.JoinDate,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                DepartmentId = employee.DepartmentId,
                DesignationId = employee.DesignationId
            };

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.EmployeeId }, employeeDto);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, EmployeeDto employeeDto)
        {
            if (id != employeeDto.EmployeeId)
            {
                return BadRequest();
            }

            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            employee.EmployeeName = employeeDto.EmployeeName;
            employee.Address = employeeDto.Address;
            employee.JoinDate = employeeDto.JoinDate;
            employee.Salary = employeeDto.Salary;
            employee.IsActive = employeeDto.IsActive;
            employee.DepartmentId = employeeDto.DepartmentId;
            employee.DesignationId = employeeDto.DesignationId;

            try
            {
                await _employeeRepository.UpdateEmployeeAsync(employee);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            await _employeeRepository.DeleteEmployeeAsync(id);

            return NoContent();
        }

        private async Task<bool> EmployeeExists(int id)
        {
            return await _employeeRepository.GetEmployeeByIdAsync(id) != null;
        }
    }
}
