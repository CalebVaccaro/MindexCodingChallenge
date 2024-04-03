using System;
using System.ComponentModel.DataAnnotations;

namespace CodeChallenge.Models;

public class Compensation
{
    [Key]
    public string Id { get; set; }
    public string EmployeeId { get; set; }
    // this is virtual to allow for lazy loading
    public virtual Employee Employee { get; set; }
    public double Salary { get; set; }
    public DateTime EffectiveDate { get; set; }

    public Compensation()
    {
        Id = Guid.NewGuid().ToString();
        EffectiveDate = DateTime.Now;
    }
}
