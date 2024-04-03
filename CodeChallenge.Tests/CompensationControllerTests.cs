using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using CodeChallenge.Contracts;
using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeChallenge.Tests.Integration;

[TestClass]
public class CompensationControllerTests
{
    private static HttpClient _httpClient;
    private static TestServer _testServer;

    [ClassInitialize]
    // Attribute ClassInitialize requires this signature
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
    public static void InitializeClass(TestContext context)
    {
        _testServer = new TestServer();
        _httpClient = _testServer.NewClient();
    }

    [ClassCleanup]
    public static void CleanUpTest()
    {
        _httpClient.Dispose();
        _testServer.Dispose();
    }
    
    [TestMethod]
    public void CreateCompensation_Returns_Ok()
    {
        // Arrange
        var compensation = new CompensationDTO()
        {
            Employee = new EmployeeDTO()
            {
                EmployeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f",
                Department = "Engineering",
                FirstName = "John",
                LastName = "Doe",
                Position = "Software Engineer",
                DirectReports = new List<string>()
                {
                    "b7839309-3348-463b-a7e3-5de1c168d4b0",
                    "03aa1462-ffa9-4978-901b-7c001562cf6f"
                }
            },
            Salary = 100000,
            EffectiveDate = new DateTime(2021, 1, 1)
        };

        var requestContent = new JsonSerialization().ToJson(compensation);

        // Execute
        var postRequestTask = _httpClient.PostAsync("api/compensation",
           new StringContent(requestContent, Encoding.UTF8, "application/json"));
        var response = postRequestTask.Result;

        // Assert
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

        var newCompensation = response.DeserializeContent<CompensationDTO>();
        Assert.AreEqual(compensation.Employee.EmployeeId, newCompensation.Employee.EmployeeId);
        Assert.AreEqual(compensation.Salary, newCompensation.Salary);
        Assert.AreEqual(compensation.EffectiveDate, newCompensation.EffectiveDate);
    }
    
    // Create CreateCompensation_Returns_NotFound
    [TestMethod]
    public void CreateCompensation_Returns_BadRequest()
    {
        // Arrange
        var nonExistentEmployeeId = "non-existent-id";
        var compensation = new CompensationDTO()
        {
            Employee = new EmployeeDTO()
            {
                EmployeeId = nonExistentEmployeeId,
                Department = "Engineering",
                FirstName = "John",
                LastName = "Doe",
                Position = "Software Engineer",
                DirectReports = new List<string>()
                {
                    "b7839309-3348-463b-a7e3-5de1c168d4b0",
                    "03aa1462-ffa9-4978-901b-7c001562cf6f"
                }
            },
            Salary = 100000,
            EffectiveDate = new DateTime(2021, 1, 1)
        };

        var requestContent = new JsonSerialization().ToJson(compensation);

        // Act
        var postRequestTask = _httpClient.PostAsync("api/compensation",
            new StringContent(requestContent, Encoding.UTF8, "application/json"));
        var response = postRequestTask.Result;

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    [TestMethod]
    public void GetCompensationByEmployeeId_Returns_Ok()
    {
        // Arrange
        var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";
        var expectedSalary = 100000;
        var expectedEffectiveDate = new DateTime(2021, 1, 1);

        var compensation = new CompensationDTO()
        {
            Employee = new EmployeeDTO()
            {
                EmployeeId = employeeId,
                Department = "Engineering",
                FirstName = "John",
                LastName = "Doe",
                Position = "Software Engineer",
                DirectReports = new List<string>()
                {
                    "b7839309-3348-463b-a7e3-5de1c168d4b0",
                    "03aa1462-ffa9-4978-901b-7c001562cf6f"
                }
            },
            Salary = expectedSalary,
            EffectiveDate = expectedEffectiveDate
        };

        var requestContent = new JsonSerialization().ToJson(compensation);

        // Execute
        var postRequestTask = _httpClient.PostAsync("api/compensation",
            new StringContent(requestContent, Encoding.UTF8, "application/json"));
        var postResponse = postRequestTask.Result;

        Assert.AreEqual(HttpStatusCode.Created, postResponse.StatusCode);

        var getRequestTask = _httpClient.GetAsync($"api/compensation/{employeeId}");
        var getResponse = getRequestTask.Result;

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, getResponse.StatusCode);

        var returnedCompensation = getResponse.DeserializeContent<CompensationDTO>();
        Assert.AreEqual(employeeId, returnedCompensation.Employee.EmployeeId);
        Assert.AreEqual(expectedSalary, returnedCompensation.Salary);
        Assert.AreEqual(expectedEffectiveDate, returnedCompensation.EffectiveDate);
    }
    
    [TestMethod]
    public void GetCompensationByEmployeeId_Returns_BadRequest()
    {
        // Arrange
        var invalidEmployeeId = "invalid-id";

        // Act
        var getRequestTask = _httpClient.GetAsync($"api/compensation/{invalidEmployeeId}");
        var response = getRequestTask.Result;
        
        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }
}