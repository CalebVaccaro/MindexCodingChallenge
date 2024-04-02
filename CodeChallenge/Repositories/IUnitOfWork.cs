using System.Threading.Tasks;
using CodeChallenge.Contracts;
using CodeChallenge.Models;
using CodeChallenge.Services;

namespace CodeChallenge.Repositories;

public interface IUnitOfWork
{
    CompensationDTO GetCompensationByIdAsync(string id);
    CompensationDTO CreateCompensationAsync(Compensation compensation);
    Task SaveAsync();
}