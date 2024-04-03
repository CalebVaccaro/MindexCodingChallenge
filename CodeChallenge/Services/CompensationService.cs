using CodeChallenge.Models;
using CodeChallenge.Repositories;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Services;

public class CompensationService : ICompensationService
{
    private readonly ICompensationRepository _compensationRepository;
    private readonly ILogger<CompensationService> _logger;

    public CompensationService(ILogger<CompensationService> logger, ICompensationRepository compensationRepository)
    {
        _compensationRepository = compensationRepository;
        _logger = logger;
    }

    public Compensation GetById(string id)
    {
        if (!string.IsNullOrEmpty(id))
        {
            return _compensationRepository.GetById(id);
        }

        _logger.LogError($"Compensation with id {id} not found.");
        return null;
    }

    public Compensation Create(Compensation compensation)
    {
        if (compensation != null)
        {
            var existingCompensation = _compensationRepository.GetById(compensation.EmployeeId);
            if (existingCompensation != null)
            {
                _logger.LogError($"Compensation for employee with id {compensation.EmployeeId} already exists.");
                return existingCompensation;
            }
            
            _compensationRepository.Add(compensation);
            _compensationRepository.SaveAsync().Wait();
        }

        return compensation;
    }
}