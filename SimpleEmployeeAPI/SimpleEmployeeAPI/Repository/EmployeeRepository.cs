using Dapper;
using SimpleEmployeeAPI.Data;
using SimpleEmployeeAPI.Model;

namespace SimpleEmployeeAPI.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DbConnectionFactory dbConnectionFactory;

        public EmployeeRepository(DbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }
        public int createEmployee(CreateEmployeeRequest createEmployeeRequest)
        {
            const string query = "select fn_create_employee(@Name,@Email,@Department,@Salary)";

            var connection = dbConnectionFactory.createConnection();
            connection.Open();

            var parameters = new
            {
                Name = createEmployeeRequest.Name,
                Email = createEmployeeRequest.Email,
                Department = createEmployeeRequest.Department,
                Salary = createEmployeeRequest.Salary
            };

            int id = connection.ExecuteScalar<int>(query, createEmployeeRequest);

            return id;
        }

        public async Task<bool> deleteEmployee(int id)
        {
            const string query = "select fn_delete_employee(@Id);";

            var connection = dbConnectionFactory.createConnection();
            connection.Open();

            bool result = await connection.ExecuteScalarAsync<bool>(query, new { Id = id});

            return result;
        }

        public async Task<IEnumerable<Employee>> getAllEmployee()
        {
            const string query = "select * from fn_get_all_employees()";

            var connection = dbConnectionFactory.createConnection();

            connection.Open();

            var employees = await connection.QueryAsync<Employee>(query);

            return employees;
        }

        public Task<Employee> getEmployeeById(int Id)
        {
            const string query = "select * from fn_get_employee_By_Id(@Id)";

            var connection = dbConnectionFactory.createConnection();
            connection.Open();

            var employee = connection.QueryFirstOrDefaultAsync<Employee>(query, new {Id = Id});

            return employee;
        }

        public async Task<bool> updateEmployee(Employee employee)
        {
            const string query = "select fn_update_employee(@Id, @Name, @Email, @Department, @Salary);";

            var connection = dbConnectionFactory.createConnection();
            connection.Open();

            bool result = await connection.ExecuteScalarAsync<bool>(query, employee);

            return result;
        }
    }
}
