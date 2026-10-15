using SimpleEmployeeAPI.Model;
using SimpleEmployeeAPI.Repository;

namespace SimpleEmployeeAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeRepository employeeRepository;

        public EmployeeService(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        public int createEmployee(CreateEmployeeRequest createEmployeeRequest)
        {
            int id = employeeRepository.createEmployee(createEmployeeRequest);

            return id;
        }

        public async Task<bool> deleteEmployee(int id)
        {
            bool result = await employeeRepository.deleteEmployee(id);

            return result;
        }

        public Task<IEnumerable<Employee>> getAllEmployee()
        {
            var employee = employeeRepository.getAllEmployee();

            return employee;
        }

        public async Task<Employee> getEmployeeById(int Id)
        {
            var employee = await employeeRepository.getEmployeeById(Id);

            return employee;
        }

        public async Task<bool> updateEmployee(Employee employee)
        {
            bool result = await employeeRepository.updateEmployee(employee);

            return result;
        }
    }
}
