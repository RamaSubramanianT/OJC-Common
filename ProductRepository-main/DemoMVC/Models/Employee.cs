namespace DemoMVC.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? DeptName { get; set; }
        public decimal Salary { get; set; }
        public string? active { get; set; }
        public bool isActive { get; set; }
        public DateTime dob { get; set; }
        public string? Gender { get; set; }
    }
}
