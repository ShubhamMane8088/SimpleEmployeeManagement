using SimpleEmployeeAPI.Model;

namespace SimpleEmployeeAPI.Repository
{
    public interface IEmployeeRepository
    {
        public int createEmployee(CreateEmployeeRequest createEmployeeRequest);
        public Task<IEnumerable<Employee>> getAllEmployee();

        Task<Employee> getEmployeeById(int Id);
        Task<bool> updateEmployee(Employee employee);

        Task<bool> deleteEmployee(int id);

    }
}
