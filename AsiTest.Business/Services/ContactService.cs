using AsiTest.Business.Contexts;
using AsiTest.Business.Entities.Contact;
using AsiTest.Business.Extensions;
using AsiTest.Business.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AsiTest.Business.Services;

/// <summary>
/// Service for CRUD operations on Contact and Email entities
/// </summary>
public class ContactService
{
    private readonly ContactRepository _contactRepository;
    private readonly EmailRepository _emailRepository;
    private readonly ApplicationContext _applicationContext;

    public ContactService(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
        _contactRepository = new ContactRepository(applicationContext);
        _emailRepository = new EmailRepository(applicationContext);
    }
    
    #region create

    /// <summary>
    /// Creates a Contact with supplied contact object
    /// </summary>
    /// <param name="contact"></param>
    public void CreateContact(Contact contact)
    {
        contact.Emails.CorrectEmails();
        
        _contactRepository.Create(contact); 
        _applicationContext.SaveChanges();
    }
    
    #endregion create
    
    #region read

    /// <summary>
    /// Returns contact with supplied id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Contact? GetContactById(long id)
    {
        // would prefer to use `_contactRepository.FindById(id);` but it will not hydrate the Emails if that is used...
        return _contactRepository.FindByCondition(c => c.Id == id).Include(c => c.Emails).FirstOrDefault();
    }

    /// <summary>
    /// Returns all contacts
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Contact> GetContacts()
    {
        return _contactRepository.FindAll().Include(c => c.Emails);
    }

    /// <summary>
    /// Returns contacts where Name contains supplied name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public IEnumerable<Contact> FindContacts(string name)
    {
        return _contactRepository.FindByCondition(c => c.Name.IndexOf(name, StringComparison.InvariantCultureIgnoreCase) > -1).Include(c => c.Emails);
    }

    /// <summary>
    /// Returns contacts whose BirthDate are between the supplied startDate and endDate
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    public IEnumerable<Contact> FindContacts(DateOnly startDate, DateOnly endDate)
    {
        return _contactRepository.FindByCondition(c => c.BirthDate >= startDate && c.BirthDate <= endDate).Include(c => c.Emails);
    }

    #endregion read
    
    #region update
    
    /// <summary>
    /// Updates a contact with the supplied contact object
    /// </summary>
    /// <param name="contact"></param>
    public void UpdateContact(Contact contact)
    {
        contact.Emails.CorrectEmails();
        
        _contactRepository.Update(contact); 
        _applicationContext.SaveChanges();
    }
    
    #endregion update
    
    #region delete

    /// <summary>
    /// Deletes a Contact with supplied id
    /// </summary>
    /// <param name="id"></param>
    public void DeleteContactById(long id)
    {
        var contact = GetContactById(id);
        if (contact != null)
        {
            _contactRepository.Delete(contact);
            _applicationContext.SaveChanges();
        }
    }
    
    #endregion delete
}