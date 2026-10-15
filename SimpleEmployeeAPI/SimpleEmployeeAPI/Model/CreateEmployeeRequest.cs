namespace SimpleEmployeeAPI.Model
{
    public class CreateEmployeeRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }
    }
}
