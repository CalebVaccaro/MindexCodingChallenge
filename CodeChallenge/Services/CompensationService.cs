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

        return null;
    }

    public Compensation Create(Compensation compensation)
    {
        if (compensation != null)
        {
            _compensationRepository.Add(compensation);
            _compensationRepository.SaveAsync().Wait();
        }

        return compensation;
    }
}