using CodeChallenge.Contracts;
using CodeChallenge.Mapping;

namespace CodeChallenge.Models;

public class ReportingStructure
{
    public Employee Employee { get; }
    public int NumberOfReports { get; }
    
    public ReportingStructure(Employee employee, int numberOfReports)
    {
        Employee = employee;
        NumberOfReports = numberOfReports;
    }
}