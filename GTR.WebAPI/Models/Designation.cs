namespace GTR.WebAPI.Models
{
    public class Designation
    {
        public int DesignationId { get; set; }
        public string DesignationName { get; set; }
        public ICollection<EmployeeInfo> Employees { get; set; }
    }
}
