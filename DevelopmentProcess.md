# Development Process

### Technology Stack
- Visual Studio 2022
- Postman

### Reviewing Code

- Reviewed readme.md and codebase to understand the project and scope requirements.
- Ran API and tested endpoints using Postman.
- Ran existing tests to validate functionality.

### Reporting Structure Endpoint

- Solved problem using recursion to calculate the number of reports for an employee and their reports.
- Created ReportingStructure type to hold employee (Employee) and numberOfReports (int).
- Added functionality to EmployeeService to utilize existing EmployeeRepository methods.
- Created ReportingStructure GET (Read) Endpoint to EmployeeController.
- Tested functionality using Postman and Unit tests.

### Compensation Endpoints

- Created Compensation type to hold Employee, Salary, and EffectiveDate.
- Added new classes CompensationRepository, CompensationService, and CompensationController to hold persistent data, without modifying existing EmployeeRepository.
- Upon further review, I realized EmployeeRepository should be required to create a new Compensation, so I added a method to EmployeeService by retrieving Employee Information.
- Created Compensation GET (Read) and POST (Create) Endpoints to EmployeeController per scope requirements.
- Removed CompensationService and EmployeeService from CompensationController to reduce redundancy instead implementing a **Unit of Work** pattern.
- Tested functionality using Postman and Unit tests.

### DTOs (Data Transfer Objects)

- Created EmployeeDto after reviewing discrepancies between Employee data structure and Employee JSON Schema for consumers.
- Followed suit with CompensationDto to alleviate Employee data structure added to the database as full object.
- Updated CompensationContext to find and map to Employee using the model builder.

### Tests
- AAA (Arrange, Act, Assert) pattern used for all tests.
- Utilized existing test framework, I am also well versed in xUnit.
- Added Unit Tests for ReportingStructure Endpoint in EmployeeControllerTests
- Created CompensationController tests to validate creating and reading compensations in CompensationControllerTests.
- Encountered race conditions using EmployeeSeedData.json data during ReportingStructure and UpdateEmployee_Returns_Ok tests
    - This was resolved by adding test data to EmployeeSeedData.json, circumventing the need to rearrange code lines.
