using System.Collections.Generic;

namespace CodeChallenge.Contracts;

// DTO for Employee entity that aligns with JSON schema
public class EmployeeDTO
{
    public string EmployeeId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Position { get; set; }
    public string Department { get; set; }
    public List<string> DirectReports { get; set; }
}
