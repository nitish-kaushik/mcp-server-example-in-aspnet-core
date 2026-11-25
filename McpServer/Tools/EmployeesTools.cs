using System.ComponentModel;
using ModelContextProtocol.Server;

namespace McpServer.Tools;

[McpServerToolType]
public sealed class EmployeesTools(EmployeeService employeeService)
{
    [McpServerTool]
    [Description("Get all employees")]
    public List<EmployeeModel> GetEmployees()
    {
        return employeeService.GetEmployees();
    }

    [McpServerTool]
    [Description("Get an employee by ID Example: 1")]
    public EmployeeModel? GetEmployeeById(
        [Description("The ID of the employee")]
        int id)
    {
        return employeeService.GetEmployeeById(id);
    }
}
