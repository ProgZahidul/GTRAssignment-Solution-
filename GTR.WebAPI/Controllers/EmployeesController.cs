using GTR.WebAPI.Data;
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
        private readonly ApplicationDbContext _context;

        public EmployeesController(IEmployeeRepository employeeRepository, ApplicationDbContext context)
        {
            _employeeRepository = employeeRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            var employees = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Designation)
                .ToListAsync();

            var employeeDtos = employees.Select(e => new EmployeeDto
            {
                EmployeeId = e.EmployeeId,
                EmployeeName = e.EmployeeName,
                Address = e.Address,
                JoinDate = e.JoinDate,
                Salary = e.Salary,
                IsActive = e.IsActive,
                DepartmentId = e.DepartmentId,
                DepartmentName = e.Department?.DepartmentName,
                DesignationId = e.DesignationId,
                DesignationName = e.Designation?.DesignationName
            });

            return Ok(employeeDtos);
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Designation)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null)
                return NotFound();

            var employeeDto = new EmployeeDto
            {
                EmployeeId = employee.EmployeeId,
                EmployeeName = employee.EmployeeName,
                Address = employee.Address,
                JoinDate = employee.JoinDate,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                DepartmentId = employee.DepartmentId,
                DepartmentName = employee.Department?.DepartmentName,
                DesignationId = employee.DesignationId,
                DesignationName = employee.Designation?.DesignationName
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
        [HttpGet("departments")]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            return await _context.Departments.ToListAsync();
        }

        [HttpGet("designations")]
        public async Task<ActionResult<IEnumerable<Designation>>> GetDesignations()
        {
            return await _context.Designations.ToListAsync();
        }
    }
}
