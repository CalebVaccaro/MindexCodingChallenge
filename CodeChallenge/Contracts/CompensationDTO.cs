using System;

namespace CodeChallenge.Contracts;

public class CompensationDTO
{
    public EmployeeDTO Employee { get; set; }
    public double Salary { get; set; }
    public DateTime EffectiveDate { get; set; }
}
