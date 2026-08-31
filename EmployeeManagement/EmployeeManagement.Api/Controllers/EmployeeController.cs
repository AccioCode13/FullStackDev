using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace EmployeeManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public EmployeeController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult GetEmployees()
    {
        var employees = new List<object>();
        string? connectionString = _configuration.GetConnectionString("EmployeeDB");
        if (string.IsNullOrEmpty(connectionString))
        {
            return StatusCode(500, "Connection string not found");
        }

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT Id, FirstName, LastName, DOB, Salary, deptId, IsActive FROM Employee";
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    employees.Add(new
                    {
                        Id = reader.GetInt32(0),
                        FirstName = reader.IsDBNull(1) ? null : reader.GetString(1),
                        LastName = reader.IsDBNull(2) ? null : reader.GetString(2),
                        DOB = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3),
                        Salary = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                        DeptId = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                        IsActive = reader.IsDBNull(6) ? (bool?)null : reader.GetBoolean(6)
                    });
                }
            }
        }

        return Ok(employees);
    }

    [HttpPost]
    public IActionResult AddEmployee([FromBody] Employee newEmployee)
    {
        string? connectionString = _configuration.GetConnectionString("EmployeeDB");
        if (string.IsNullOrEmpty(connectionString))
        {
            return StatusCode(500, "Connection string not found.");
        }

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // Check if this Id already exists BEFORE trying to insert
            string checkQuery = "SELECT COUNT(*) FROM Employee WHERE Id = @Id";
            using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
            {
                checkCommand.Parameters.AddWithValue("@Id", newEmployee.Id);
                int existingCount = (int)checkCommand.ExecuteScalar();
                if (existingCount > 0)
                {
                    return Conflict($"Employee with Id {newEmployee.Id} already exists. Try a different Id.");
                }
            }

            string query = "INSERT INTO Employee (Id, FirstName, LastName, DOB, Salary, DeptId, IsActive) VALUES (@Id, @FirstName, @LastName, @DOB, @Salary, @DeptId, @IsActive)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", newEmployee.Id);
                command.Parameters.AddWithValue("@FirstName", newEmployee.FirstName);
                command.Parameters.AddWithValue("@LastName", newEmployee.LastName);
                command.Parameters.AddWithValue("@DOB", newEmployee.DOB);
                command.Parameters.AddWithValue("@Salary", newEmployee.Salary);
                command.Parameters.AddWithValue("@DeptId", newEmployee.DeptId);
                command.Parameters.AddWithValue("@IsActive", newEmployee.IsActive);
                command.ExecuteNonQuery();
            }
        }
        return Ok(newEmployee);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateEmployee(int id, [FromBody] Employee updatedEmployee)
    {
        string? connectionString = _configuration.GetConnectionString("EmployeeDB");
        if (string.IsNullOrEmpty(connectionString))
        {
            return StatusCode(500, "Connection string not found.");
        }

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "UPDATE Employee SET FirstName = @FirstName, LastName = @LastName, DOB = @DOB, Salary = @Salary, DeptId = @DeptId, IsActive = @IsActive WHERE Id = @Id";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@FirstName", updatedEmployee.FirstName);
                command.Parameters.AddWithValue("@LastName", updatedEmployee.LastName);
                command.Parameters.AddWithValue("@DOB", updatedEmployee.DOB);
                command.Parameters.AddWithValue("@Salary", updatedEmployee.Salary);
                command.Parameters.AddWithValue("@DeptId", updatedEmployee.DeptId);
                command.Parameters.AddWithValue("@IsActive", updatedEmployee.IsActive);
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    return NotFound($"No employee found with Id {id}");
                }
            }
        }
        return Ok(updatedEmployee);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteEmployee(int id)
    {
        string? connectionString = _configuration.GetConnectionString("EmployeeDB");
        if (string.IsNullOrEmpty(connectionString))
        {
            return StatusCode(500, "Connection string not found");
        }
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "DELETE FROM Employee WHERE Id = @Id";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    return NotFound($"No employee found with Id {id}");
                }
            }
        }
        return NoContent();
    }
}

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DOB { get; set; }
    public int Salary { get; set; }
    public int DeptId { get; set; }
    public bool IsActive { get; set; }
}