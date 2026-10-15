using Microsoft.AspNetCore.Mvc;
using SimpleEmployeeAPI.Data;
using SimpleEmployeeAPI.Model;
using SimpleEmployeeAPI.Services;

namespace SimpleEmployeeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly DbConnectionFactory _dbConnectionFactory;
        private readonly EmployeeService employeeService;


        public EmployeeController(DbConnectionFactory dbConnectionFactory, EmployeeService employeeService)
        {
            this._dbConnectionFactory = dbConnectionFactory;
            this.employeeService = employeeService;
        }

        [HttpGet("testDbConnection")]
        public IActionResult GetConnection()
        {
            try
            {
                var connection = _dbConnectionFactory.createConnection();

                connection.Open();

                return Ok(new
                {
                    message = "SQL connection successful"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "SQL connection failed"
                });
            }
        }

        [HttpPost]
        public IActionResult createEmployee(CreateEmployeeRequest createEmployeeRequest)
        {
            int id = employeeService.createEmployee(createEmployeeRequest);

            return Ok(new
            {
                message = "Employee created successfully...",
                id
            });
        }

        [HttpGet("getAllEmployees")]
        public async Task<IActionResult> getAllEmployees()
        {
            var employees = await employeeService.getAllEmployee();

            return Ok(employees);
        }

        [HttpGet("getEmployeeById")]
        public async Task<IActionResult> getEmployeeById(int id)
        {
            var employeeById = await employeeService.getEmployeeById(id);

            if(employeeById == null)
            {
                return NotFound();
            }

            return Ok(employeeById);
        }

        [HttpPut("updateEmployee/{id}")]
        public async Task<IActionResult> updateEmployee(int id,CreateEmployeeRequest request)
        {
            Employee employee = new Employee
            {
                Id = id,
                Name = request.Name,
                Department = request.Department,
                Email = request.Email,
                Salary = request.Salary,
            };

            bool result = await employeeService.updateEmployee(employee);

            if (!result)
            {
                return NotFound(new
                {
                    message = "Employee not found..."
                });
            }


            return Ok(new
            {
                message = "Employee updated successfully..."
            });
        }

        [HttpDelete("deleteEmployee/{id}")]
        public async Task<IActionResult> deleteEmployee(int id)
        {
            bool result = await employeeService.deleteEmployee(id);

            if (!result)
            {
                return NotFound();
            }

            return Ok(new
            {
                message = "Employee deleted successfully..."
            });
        }
    }
}
