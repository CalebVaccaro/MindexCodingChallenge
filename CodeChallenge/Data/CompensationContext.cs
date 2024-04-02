using CodeChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Data;

public class CompensationContext : DbContext
{
    public CompensationContext(DbContextOptions<CompensationContext> options) : base(options)
    {
    }

    public DbSet<Compensation> Compensations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Compensation>()
            // Correctly points to the Employee navigation property
            .HasOne(c => c.Employee)
            .WithMany()
            // Specifies EmployeeId as the foreign key
            .HasForeignKey(c => c.EmployeeId);
    }
}
