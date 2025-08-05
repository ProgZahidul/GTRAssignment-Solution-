namespace GTR.WebAPI.Dtos
{
 
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Address { get; set; }
        public DateTime JoinDate { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }
    }
}
