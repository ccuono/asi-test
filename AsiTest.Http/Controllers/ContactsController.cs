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
public class ContactsController : ControllerBase
{
    private readonly IContactService _contactService;

    /// <summary>
    /// Constructor
    /// Removed `ILogger&lt;ContactsController&gt; logger` since it is not used
    /// </summary>
    /// <param name="contactService"></param>
    public ContactsController(IContactService contactService)
    {
        _contactService = contactService;
    }

    /// <summary>
    /// Creates a new Contact
    /// </summary>
    /// <param name="contact"></param>
    /// <returns></returns>
    [HttpPost(Name = "CreateContact")]
    public Contact Post(Contact contact)
    {
        _contactService.CreateContact(contact);
        return contact;
    }

    /// <summary>
    /// Returns a list of all Contacts in the database OR
    /// Returns a list of Contacts whose birthday are between the startDate and endDate
    /// Use ISO 8601 formatted strings for both startDate and endDate
    /// </summary>
    /// <returns></returns>
    [HttpGet(Name = "GetContacts")]
    public IEnumerable<Contact> Get(DateOnly? startDate = null, DateOnly? endDate = null)
    {
        if (startDate == null || endDate == null)
        {
            return _contactService.GetContacts();
        }
        else
        {
            return _contactService.FindContacts(startDate.Value, endDate.Value);
        }
    }

    /// <summary>
    /// Returns a the contact with supplied id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    
    [HttpGet("{id:long}", Name = "GetContactById")]
    public Contact? Get(long id)
    {
        return _contactService.GetContactById(id);
    }

    /// <summary>
    /// Returns a list of Contacts whose name contains the supplied name string
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    
    [HttpGet("{name}", Name = "FindContactByName")]
    public IEnumerable<Contact> Get(string name)
    {
        return _contactService.FindContacts(name);
    }

    /// <summary>
    /// Updates a Contact
    /// </summary>
    /// <param name="contact"></param>
    /// <returns></returns>
    [HttpPut(Name = "UpdateContact")]
    public Contact Put(Contact contact)
    {
        _contactService.UpdateContact(contact);
        return contact;
    }

    /// <summary>
    /// Deletes a Contact with the supplied id
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete(Name = "DeleteContactById")]
    public void Delete(long id)
    {
        _contactService.DeleteContactById(id);
    }
}