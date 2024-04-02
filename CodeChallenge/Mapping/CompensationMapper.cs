using System.Linq;
using CodeChallenge.Contracts;
using CodeChallenge.Models;

namespace CodeChallenge.Mapping;

public static class CompensationMapper
{
    public static CompensationDTO ToCompensationDTO(this Compensation compensation, Employee employee)
    {
        return new CompensationDTO
        {
            Employee = new EmployeeDTO
            {
                EmployeeId = compensation.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Position = employee.Position,
                Department = employee.Department,
                DirectReports = employee.DirectReports.Select(e => e.EmployeeId).ToList()
            },
            Salary = compensation.Salary,
            EffectiveDate = compensation.EffectiveDate
        };
    }

    public static Compensation ToCompensation(this CompensationDTO compensationDTO)
    {
        return new Compensation
        {
            EmployeeId = compensationDTO.Employee.EmployeeId, // Assuming EmployeeDTO is properly populated
            Salary = compensationDTO.Salary,
            EffectiveDate = compensationDTO.EffectiveDate
        };
    }
}