namespace GTR.WebAPI.Models
{

    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public ICollection<EmployeeInfo> Employees { get; set; }
    }
}
