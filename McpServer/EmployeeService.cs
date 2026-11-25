namespace McpServer;

public class EmployeeService
{
    private List<EmployeeModel> employees =
    [
        new EmployeeModel() { Id = 1, Name = "John Doe", Email = "john.doe@example.com" },
        new EmployeeModel() { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com" },
        new EmployeeModel() { Id = 3, Name = "Alice Johnson", Email = "alice.johnson@example.com" }
    ];

    public List<EmployeeModel> GetEmployees()
    {
        return employees;
    }

    public EmployeeModel? GetEmployeeById(int id)
    {
        return employees.FirstOrDefault(e => e.Id == id);
    }
}
