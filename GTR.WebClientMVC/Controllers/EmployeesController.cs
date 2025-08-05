using GTR.WebClientMVC.Models;
using GTR.WebClientMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GTR.WebClientMVC.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApiService _apiService;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(ApiService apiService, ILogger<EmployeesController> logger)
        {
            _apiService = apiService;
            _logger = logger;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            try
            {
                var employees = await _apiService.GetEmployeesAsync();
                return View(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting employees");
                return View(new List<EmployeeViewModel>());
            }
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var employee = await _apiService.GetEmployeeAsync(id);
                if (employee == null) return NotFound();
                return View(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting employee with id {id}");
                return NotFound();
            }
        }

        // GET: Employees/Create
        public async Task<IActionResult> Create()
        {
            await LoadDropdowns();
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _apiService.CreateEmployeeAsync(employee);
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Employee created successfully";
                        return RedirectToAction(nameof(Index));
                    }
                    ModelState.AddModelError(string.Empty, "Error creating employee");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating employee");
                    ModelState.AddModelError(string.Empty, "Error creating employee");
                }
            }
            await LoadDropdowns();
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var employee = await _apiService.GetEmployeeAsync(id);
                if (employee == null) return NotFound();
                await LoadDropdowns();
                return View(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting employee for edit with id {id}");
                return NotFound();
            }
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeViewModel employee)
        {
            if (id != employee.EmployeeId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _apiService.UpdateEmployeeAsync(employee);
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Employee updated successfully";
                        return RedirectToAction(nameof(Index));
                    }
                    ModelState.AddModelError(string.Empty, "Error updating employee");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error updating employee with id {id}");
                    ModelState.AddModelError(string.Empty, "Error updating employee");
                }
            }
            await LoadDropdowns();
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var employee = await _apiService.GetEmployeeAsync(id);
                if (employee == null) return NotFound();
                return View(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting employee for delete with id {id}");
                return NotFound();
            }
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var response = await _apiService.DeleteEmployeeAsync(id);
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Employee deleted successfully";
                    return RedirectToAction(nameof(Index));
                }
                TempData["ErrorMessage"] = "Error deleting employee";
                return RedirectToAction(nameof(Delete), new { id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting employee with id {id}");
                TempData["ErrorMessage"] = "Error deleting employee";
                return RedirectToAction(nameof(Delete), new { id });
            }
        }

        private async Task LoadDropdowns()
        {
            try
            {
                var departments = await _apiService.GetDepartmentsAsync() ?? new List<DepartmentViewModel>();
                var designations = await _apiService.GetDesignationsAsync() ?? new List<DesignationViewModel>();

                ViewBag.DepartmentList = new SelectList(departments, "DepartmentId", "DepartmentName");
                ViewBag.DesignationList = new SelectList(designations, "DesignationId", "DesignationName");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading dropdowns");
                ViewBag.DepartmentList = new SelectList(new List<DepartmentViewModel>(), "DepartmentId", "DepartmentName");
                ViewBag.DesignationList = new SelectList(new List<DesignationViewModel>(), "DesignationId", "DesignationName");
            }
        }
    }
}
