using System.ComponentModel.DataAnnotations;

namespace GTR.WebClientMVC.Models
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }

        [Required]
        public string EmployeeName { get; set; }

        public string Address { get; set; }

        [DataType(DataType.Date)]
        public DateTime JoinDate { get; set; } = DateTime.Today;

        [Range(0, double.MaxValue)]
        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [Display(Name = "Designation")]
        public int DesignationId { get; set; }

        public DepartmentViewModel Department { get; set; }
        public DesignationViewModel Designation { get; set; }
    }
}
