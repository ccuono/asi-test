using AsiTest.Business.Entities.Contact;
using AsiTest.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace AsiTest.Http.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactController : ControllerBase
{
    private readonly ILogger<ContactController> _logger;
    private readonly ContactService _contactService;

    public ContactController(ILogger<ContactController> logger, ContactService contactService)
    {
        _logger = logger;
        _contactService = contactService;
    }

    /// <summary>
    /// Returns a list of all contacts in the database
    /// </summary>
    /// <returns></returns>
    [HttpGet(Name = "GetContacts")]
    public IEnumerable<Contact> Get()
    {
        return _contactService.GetContacts();
    }

    /// <summary>
    /// Returns a list of contacts whose name contains the supplied name string
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpGet(Name = "FindContactByName")]
    public IEnumerable<Contact> Get(string name)
    {
        return _contactService.FindContacts(name);
    }

    /// <summary>
    /// Returns a list of contacts whose birthday are between the startDate and endDate
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    [HttpGet(Name = "FindContactByBirthDateRange")]
    public IEnumerable<Contact> Get(DateOnly startDate, DateOnly endDate)
    {
        return _contactService.FindContacts(startDate, endDate);
    }
}