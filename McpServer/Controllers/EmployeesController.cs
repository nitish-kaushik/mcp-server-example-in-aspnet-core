using Microsoft.AspNetCore.Mvc;

namespace McpServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController(EmployeeService employeeService) : ControllerBase
{
    [HttpGet("")]
    public IActionResult GetAllEmployees()
    {
        var employees = employeeService.GetEmployees();
        return Ok(employees);
    }
}
