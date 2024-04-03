using System.Threading.Tasks;
using CodeChallenge.Contracts;
using CodeChallenge.Data;
using CodeChallenge.Mapping;
using CodeChallenge.Models;
using CodeChallenge.Services;

namespace CodeChallenge.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly EmployeeContext _employeeContext;
    private readonly CompensationContext _compensationContext;
    private readonly IEmployeeService _employeeService;
    private readonly ICompensationService _compensationService;
    
    public UnitOfWork(EmployeeContext employeeContext,
        CompensationContext compensationContext,
        IEmployeeService employeeService,
        ICompensationService compensationService)
    {
        _employeeContext = employeeContext;
        _compensationContext = compensationContext;
        _employeeService = employeeService;
        _compensationService = compensationService;
    }

    public CompensationDTO GetCompensationByIdAsync(string id)
    {
        var employee = _employeeService.GetById(id);
        if (employee == null)
        {
            return null;
        }
        
        var compensation = _compensationService.GetById(id);
        var compensationDto = compensation.ToCompensationDTO(employee);

        return compensationDto;
    }

    public CompensationDTO CreateCompensationAsync(Compensation compensation)
    {
        var employee = _employeeService.GetById(compensation.EmployeeId);
        if (employee == null)
        {
            return null;
        }

        var _compensation = _compensationService.Create(compensation);
        var compensationDto = _compensation.ToCompensationDTO(employee);
        
        return compensationDto;
    }

    public async Task SaveAsync()
    {
        await _employeeContext.SaveChangesAsync();
        await _compensationContext.SaveChangesAsync();
    }
}