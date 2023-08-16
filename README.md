# ASI Test

This is an application written to satisfy a set of requirements provided by ASI. The provided requirements are at the end of this file.

In order to run this application there are a couple of options, because of the utilized `InMemory` database each application would have it's own exclusive database.

1. WebApi: Simply run the WebApi application project `AsiTest.Http` in debug mode and use the Swagger UI to perform CRUD operations on the automatically created data.
2. Console Application: Simply run the console application project `AsiTest.Console` and follow the menu prompts.

> For both of these applications, especially when using Swagger, use caution with the provided methods to automatically generate Contacts. If you generate too many contacts you can really start to inflate your memory usage quickly.

When each application is run the `ApplicationDataSeeder` will go ahead and seed data into the database based on how the `appsettings.json` file is configured for each project:

```json
"database": {
    "name": "AsiTestDb",
    "seed": true,
    "contactGenerationAmount": 10,
    "emailMax": 5
  }
```

`seed` should be set to true to enable the seeding behavior, `contactGenerationAmount` is the amount of contacts that should be generated, and `emailMax` is the maximum amount of emails a contact could possibly have generated.

For this application, I've opted to directly utilize domain models instead of adding overhead with object model mapping. There is pros and cons to this for which the biggest con is that domain objects can be polluted with additional annotations to hide certain properties as time moves forward. Also, I wanted to keep the domain objects listed within the requirement remain pure to the spec.

# Requirements

## Task

Create a sample .NET Core or .NET 6 web API application with simple CRUD 
operations via Entity Framework Core.

## Requirements

- Use in-memory database
- Seed the database with some basic sample information
- Controller should have Create, Read, Update, Delete, and Search operations.
  - Searching should allow searching by name and birth date range
- Business logic should be accessible to a separate Console application
- When a contact has any emails, one and only one should be set to IsPrimary = true
- No time limit
- You can use any sources necessary to complete the task (google, stack overflow, MS documentation)
- Push to a github, gitlab, or other public git provider to submit

Use the following model for the record:
 
```csharp
public class Contact

{

    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly? BirthDate { get; set; } = null!;

    public ICollection<Email> Emails { get; set; } = null!;

}

public class Email

{

    public long Id { get; set; }

    public bool IsPrimary { get; set; }

    public string Address { get; set; } = default!;

}
```
