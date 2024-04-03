using System.Linq;
using CodeChallenge.Contracts;
using CodeChallenge.Models;

namespace CodeChallenge.Mapping;

public static class EmployeeMapper
{
    public static Employee ToEmployee(this EmployeeDTO employeeDTO)
    {
        return new Employee
        {
            EmployeeId = employeeDTO.EmployeeId,
            FirstName = employeeDTO.FirstName,
            LastName = employeeDTO.LastName,
            Position = employeeDTO.Position,
            Department = employeeDTO.Department,
            // foreach string id in employeeDTO.DirectReports
            // create a new employee with that id (replicating EmployeeSeedData.json format)
            DirectReports = employeeDTO.DirectReports?.Select(id => new Employee { EmployeeId = id }).ToList()
        };
    }

    public static EmployeeDTO ToEmployeeDTO(this Employee employee)
    {
        return new EmployeeDTO
        {
            EmployeeId = employee.EmployeeId,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Position = employee.Position,
            Department = employee.Department,
            DirectReports = employee.DirectReports?.Select(e => e.EmployeeId).ToList()
        };
    }
}