using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Data;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Repositories;

// For Persistent data, we require a repository to handle database operations
public class CompensationRepository : ICompensationRepository
{
    private readonly CompensationContext _compensationContext;
    private readonly ILogger<ICompensationRepository> _logger;

    public CompensationRepository(ILogger<ICompensationRepository> logger, CompensationContext compensationContext)
    {
        _compensationContext = compensationContext;
        _logger = logger;
    }

    public Compensation GetById(string id)
    {
        return _compensationContext.Compensations.SingleOrDefault(e => e.EmployeeId == id);
    }

    public Compensation Add(Compensation compensation)
    {
        _compensationContext.Compensations.Add(compensation);
        return compensation;
    }

    public Task SaveAsync()
    {
        return _compensationContext.SaveChangesAsync();
    }
}
