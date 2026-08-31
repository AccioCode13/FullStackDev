using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace EmployeeManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public DepartmentsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult GetDepartments()
    {
        var departments = new List<object>();
         string? connectionString = _configuration.GetConnectionString("EmployeeDB");
    if (string.IsNullOrEmpty(connectionString))
    {
        return StatusCode(500, "Connection string not found.");
    }

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT Id, deptName FROM Departments";
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    departments.Add(new
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    });
                }
            }
        }

        return Ok(departments);
    }

    [HttpPost]
    public IActionResult AddDepartment([FromBody] Department newDepartment)
    {
        string? connectionString=_configuration.GetConnectionString("EmployeeDB");
        if (string.IsNullOrEmpty(connectionString))
        {
            return StatusCode(500,"Connection string not found.");
        }

        using(SqlConnection connection=new SqlConnection(connectionString))
        {
            connection.Open();
            string query="INSERT INTO Departments (Id,deptName) VALUES (@Id, @deptName)";
            using (SqlCommand command =new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id",newDepartment.Id);
                command.Parameters.AddWithValue("@deptName",newDepartment.Name);
                command.ExecuteNonQuery();
            }
        }
        return Ok(newDepartment);
    }
    [HttpPut("{id}")]
    public IActionResult UpdateDepartment(int id,[FromBody] Department updatedDepartment)
    {
        string? connectionString=_configuration.GetConnectionString("EmployeeDB");
        if (string.IsNullOrEmpty(connectionString))
        {
            return StatusCode(500,"Connection string not found.");
        }

        using (SqlConnection connection=new SqlConnection(connectionString))
        {
            connection.Open();
            string query="UPDATE Departments SET deptName= @deptName WHERE Id=@Id";
            using(SqlCommand command=new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@deptName",updatedDepartment.Name);
                command.Parameters.AddWithValue("@Id",id);
                int rowsAffected=command.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    return NotFound($"No department found with Id {id}");
                }
            }
        }
        return Ok(updatedDepartment);

                
    }
    [HttpDelete("{id}")]
    public IActionResult DeleteDepartment(int id)
    {
        string? connectionString=_configuration.GetConnectionString("EmployeeDB");
        if (string.IsNullOrEmpty(connectionString))
        {
            return StatusCode(500,"Connection string not found.");
        }

        using (SqlConnection connection=new SqlConnection(connectionString))
        {
            connection.Open();
            string query="DELETE FROM Departments WHERE Id=@Id";
            using(SqlCommand command=new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id",id);
                int rowsAffected=command.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    return NotFound($"No department found with Id {id}");
                }
            }
        }
        return NoContent();

                
    }
}
public class Department
{
    public int Id{get;set;}
    public string Name {get;set;}= string.Empty;
}