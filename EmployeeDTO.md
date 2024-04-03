# EmployeeDTO

## Discrepancy between Employee Object and JSON Schema

- While reviewing the scope requirements, I noticed that the Employee object has a discrepancy between the object and the JSON schema. The Employee object has a `List<Employee>` field, while the JSON schema is represented as a `List<string>`.
- During my time at my previous positions, we made sure our consumers were consuming and sending data the correct way.
- If the JSON schema is incorrect, it could lead to data corruption and loss of data integrity.
- In this branch, I implemented the correct JSON schema format by enforcing an EmployeeDTO object that utilizes the List<string> field.
- The API then consumes this object and converts the DTO object to the Employee object.
- This ensures that the data is being consumed and sent correctly and our API can still utilize the Employee object with List<Employee>.
- I updated the already existing tests to reflect the changes made to the API.
- The EmployeeMapper class uses a nullable check to verify if the DirectReports field is null or not.
- I was hesitant to make the changes in the master branch without consulting the team, so I created a new branch to implement the changes.
- Please provide feedback on the changes made and let me know if you would like me to merge the changes.

The JSON Schema in the ReadMe.md

```json
{
  "type":"Employee",
  "properties": {
    "employeeId": {
      "type": "string"
    },
    "firstName": {
      "type": "string"
    },
    "lastName": {
          "type": "string"
    },
    "position": {
          "type": "string"
    },
    "department": {
          "type": "string"
    },
    // This explicitly says an array of strings
    "directReports": {
      "type": "array",
      "items" : "string"
    }
  }
}
```

While the Employee Object expects a List<Employee>

```csharp
public class Employee
{
    public String EmployeeId { get; set; }
    public String FirstName { get; set; }
    public String LastName { get; set; }
    public String Position { get; set; }
    public String Department { get; set; }
    public List<Employee> DirectReports { get; set; }
}
```

Example Response for `List<Employee>` implementation for a POST request

```json
{
  "employeeId": "16a596ae-edd3-4847-99fe-c4518e82c86f",
  "firstName": "John",
  "lastName": "Lennon",
  "position": "Development Manager",
  "department": "Engineering",
  "directReports": [
    {
      "employeeId": "b7839309-3348-463b-a7e3-5de1c168beb3"
    },
    {
      "employeeId": "03aa1462-ffa9-4978-901b-7c001562cf6f"
    }
  ]
}
```
Example Response for `List<string>` implementation for a POST request
Example Response
```json
{
    "employeeId": "c500b221-dc35-404d-ab77-2a5943520636",
    "firstName": "Roberto",
    "lastName": "Evans",
    "position": "Senior Manager",
    "department": "Product Development",
    "directReports": [
        "24e4b010-456e-408a-92f4-93fade1d061d",
        "efbfc71e-6d5e-4e70-b95e-8a376eb1c14d"
    ]
}
```