using SimpleEmployeeAPI.Model;

namespace SimpleEmployeeAPI.Services
{
    public interface IEmployeeService
    {
        int createEmployee(CreateEmployeeRequest createEmployeeRequest);

        Task<IEnumerable<Employee>> getAllEmployee();

        Task<bool> updateEmployee(Employee employee);

        Task<bool> deleteEmployee(int id);
    }
}
