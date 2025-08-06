using System.ComponentModel.DataAnnotations;

namespace GTR.WebClientMVC.Models
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Employee Name is required")]
        public string EmployeeName { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime JoinDate { get; set; } = DateTime.Today;

        [Range(0, double.MaxValue, ErrorMessage = "Salary must be non-negative")]
        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Department is required")]
        public int? DepartmentId { get; set; }

        [Required(ErrorMessage = "Designation is required")]
        public int? DesignationId { get; set; }

        public string? DepartmentName { get; set; }
        public string? DesignationName { get; set; }
        // Navigation properties — should NOT be used in model validation
        public DepartmentViewModel? Department { get; set; }
        public DesignationViewModel? Designation { get; set; }
    }
}
