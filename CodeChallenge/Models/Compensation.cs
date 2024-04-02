using System;
using System.ComponentModel.DataAnnotations;

namespace CodeChallenge.Models;

public class Compensation
{
    [Key]
    public string Id { get; set; }
    public string EmployeeId { get; set; }
    public virtual Employee Employee { get; set; } // Navigation property
    public double Salary { get; set; }
    public DateTime EffectiveDate { get; set; }

    public Compensation()
    {
        Id = Guid.NewGuid().ToString();
        EffectiveDate = DateTime.Now;
    }
}
