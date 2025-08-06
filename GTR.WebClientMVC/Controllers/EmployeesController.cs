using GTR.WebClientMVC.Models;
using GTR.WebClientMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;

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
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadDropdowns();
            return View(new EmployeeViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel employee)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdowns(employee);
                return View(employee);
            }

            var (success, error) = await _apiService.CreateEmployeeAsync(employee);

            if (success)
            {
                TempData["SuccessMessage"] = "Employee created successfully";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, error ?? "Failed to create employee");
            await LoadDropdowns(employee);
            return View(employee);
        }

        private async Task LoadDropdowns(EmployeeViewModel model = null)
        {
            var departments = await _apiService.GetDepartmentsAsync();
            var designations = await _apiService.GetDesignationsAsync();

            ViewBag.DepartmentList = new SelectList(departments, "DepartmentId", "DepartmentName", model?.DepartmentId);
            ViewBag.DesignationList = new SelectList(designations, "DesignationId", "DesignationName", model?.DesignationId);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _apiService.GetEmployeeAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            await LoadDropdowns();
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeViewModel employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                await LoadDropdowns();
                return View(employee);
            }

            var (success, error) = await _apiService.UpdateEmployeeAsync(employee);

            if (success)
            {
                TempData["SuccessMessage"] = "Employee updated successfully";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, error ?? "Failed to update employee");
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

        //private async Task LoadDropdowns(EmployeeViewModel model = null)
        //{
        //    try
        //    {
        //        // Get both datasets in parallel for better performance
        //        var departmentsTask = _apiService.GetDepartmentsAsync();
        //        var designationsTask = _apiService.GetDesignationsAsync();
        //        await Task.WhenAll(departmentsTask, designationsTask);

        //        // Determine if we're in Edit mode by checking the current action
        //        var isEditAction = ControllerContext.ActionDescriptor.ActionName.Equals("Edit", StringComparison.OrdinalIgnoreCase);

        //        ViewBag.DepartmentList = new SelectList(
        //            departmentsTask.Result ?? new List<DepartmentViewModel>(),
        //            "DepartmentId",
        //            "DepartmentName",
        //            isEditAction && model != null ? model.DepartmentId : null
        //        );

        //        ViewBag.DesignationList = new SelectList(
        //            designationsTask.Result ?? new List<DesignationViewModel>(),
        //            "DesignationId",
        //            "DesignationName",
        //            isEditAction && model != null ? model.DesignationId : null
        //        );
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error loading dropdowns");
        //        // Provide fallback empty lists
        //        ViewBag.DepartmentList = new SelectList(new List<DepartmentViewModel>(), "DepartmentId", "DepartmentName");
        //        ViewBag.DesignationList = new SelectList(new List<DesignationViewModel>(), "DesignationId", "DesignationName");
        //    }
        //}
    }
}
