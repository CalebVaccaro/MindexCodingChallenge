using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Services;
using CodeChallenge.Models;
using CodeChallenge.Contracts;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        [HttpPost]
        public IActionResult CreateEmployee([FromBody]EmployeeDTO employeeDto)
        {
            // Convert DTO to Employee
            var employee = employeeDto.ToEmployee();
            
            _logger.LogDebug($"Received employee create request for '{employee.FirstName} {employee.LastName}'");

            _employeeService.Create(employee);
            
            // Convert to Dto
            var employeeResponse = employee.ToEmployeeDTO();
            
            return CreatedAtRoute("getEmployeeById", new { id = employeeResponse.EmployeeId }, employeeResponse);
        }

        [HttpGet("{id}", Name = "getEmployeeById")]
        public IActionResult GetEmployeeById(String id)
        {
            _logger.LogDebug($"Received employee get request for '{id}'");

            var employee = _employeeService.GetById(id);

            if (employee == null)
                return NotFound();

            // Convert to Dto
            var employeeDto = employee.ToEmployeeDTO();
            
            return Ok(employeeDto);
        }

        [HttpPut("{id}")]
        public IActionResult ReplaceEmployee(String id, [FromBody]EmployeeDTO employeeDto)
        {
            // Convert DTO to Employee
            var newEmployee = employeeDto.ToEmployee();
            
            _logger.LogDebug($"Recieved employee update request for '{id}'");

            var existingEmployee = _employeeService.GetById(id);
            if (existingEmployee == null)
                return NotFound();

            _employeeService.Replace(existingEmployee, newEmployee);
            
            // Convert to Dto
            var employeeResponse = newEmployee.ToEmployeeDTO();

            return Ok(employeeResponse);
        }

        /// <summary>
        /// ReportingStructure Endpoint
        /// Return the Number of Reports for a given employee
        /// If more reporting structures endpoints are needed, consider moving to a separate controller
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/reportingStructure", Name = "getReportingStructure")]
        public IActionResult GetReportingStructure(string id)
        {
            _logger.LogDebug($"Received reporting structure get request for '{id}'");

            var employee = _employeeService.GetById(id);
            if (employee == null) return NotFound();

            // Get the Number of Reports for the given employee
            var numberOfReports = _employeeService.GetNumberOfReports(employee);

            // Convert to ReportingStructure Object
            var reportingStructure = new ReportingStructure(employee, numberOfReports);

            return Ok(reportingStructure);
        }
    }
}
