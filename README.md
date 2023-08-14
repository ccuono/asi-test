# Task

Create a sample .NET Core or .NET 6 web API application with simple CRUD 
operations via Entity Framework Core.

# Requirements

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
