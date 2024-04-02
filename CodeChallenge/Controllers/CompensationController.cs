using CodeChallenge.Contracts;
using CodeChallenge.Mapping;
using CodeChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompensationController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly ICompensationService _compensationService;
    private readonly IEmployeeService _employeeService;

    public CompensationController(ILogger<EmployeeController> logger, ICompensationService compensationService,
        IEmployeeService employeeService)
    {
        _logger = logger;
        _compensationService = compensationService;
        _employeeService = employeeService;
    }

    [HttpGet("{id}", Name = "getCompensationById")]
    public IActionResult GetCompensationById(string id)
    {
        _logger.LogDebug($"Received compensation get request for '{id}'");

        var compensation = _compensationService.GetById(id);

        if (compensation == null)
            return NotFound();

        var employee = _employeeService.GetById(id);

        if (employee == null)
            return NotFound();

        return Ok(compensation.ToCompensationDTO(employee));
    }

    [HttpPost(Name = "createCompensation")]
    public IActionResult CreateCompensation([FromBody] CompensationDTO compensationDto)
    {
        var compensation = compensationDto.ToCompensation();

        _logger.LogDebug($"Received compensation create request for '{compensation.EmployeeId}'");

        _compensationService.Create(compensation);

        var employee = _employeeService.GetById(compensation.EmployeeId);

        if (employee == null)
            return NotFound();

        return CreatedAtRoute("getCompensationById", new { id = compensation.EmployeeId }, compensation.ToCompensationDTO(employee));
    }
}
