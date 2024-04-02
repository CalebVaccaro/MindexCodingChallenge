using CodeChallenge.Contracts;
using CodeChallenge.Mapping;
using CodeChallenge.Repositories;
using CodeChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompensationController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IUnitOfWork _unitOfWork;

    public CompensationController(ILogger<EmployeeController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    [HttpGet("{id}", Name = "getCompensationById")]
    public IActionResult GetCompensationById(string id)
    {
        _logger.LogDebug($"Received compensation get request for '{id}'");

        var compensation = _unitOfWork.GetCompensationByIdAsync(id);

        if (compensation == null)
            return NotFound();

        return Ok(compensation);
    }

    [HttpPost(Name = "createCompensation")]
    public IActionResult CreateCompensation([FromBody] CompensationDTO compensationDto)
    {
        var compensation = compensationDto.ToCompensation();

        _logger.LogDebug($"Received compensation create request for '{compensation.EmployeeId}'");

        var newCompensationDto = _unitOfWork.CreateCompensationAsync(compensation);

        return CreatedAtRoute("getCompensationById", new { id = compensation.EmployeeId }, newCompensationDto);
    }
}
