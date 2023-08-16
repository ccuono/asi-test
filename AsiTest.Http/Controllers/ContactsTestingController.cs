using AsiTest.Business.Data.Testing;
using AsiTest.Business.Entities.Contact;
using AsiTest.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace AsiTest.Http.Controllers;

/// <summary>
/// Contact controller for CRUD operations for Contacts
/// </summary>
[ApiController]
[Route("[controller]")]
public class ContactsTestingController : ControllerBase
{
    private readonly IContactService _contactService;

    /// <summary>
    /// Constructor
    /// Removed `ILogger&lt;ContactsController&gt; logger` since it is not used
    /// </summary>
    /// <param name="contactService"></param>
    public ContactsTestingController(IContactService contactService)
    {
        _contactService = contactService;
    }

    /// <summary>
    /// Generates randomized contacts
    /// </summary>
    /// <param name="contactGenerationAmount"></param>
    /// <param name="emailMax"></param>
    /// <returns></returns>
    [HttpPost(Name = "GenerateContacts")]
    public IEnumerable<Contact> Post(int contactGenerationAmount, int emailMax)
    {
        var applicationDataSeeder = new ApplicationDataSeeder(_contactService);
        applicationDataSeeder.InitializeData(contactGenerationAmount, emailMax);

        return _contactService.GetContacts();
    }
}