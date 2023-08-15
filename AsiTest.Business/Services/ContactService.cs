using AsiTest.Business.Contexts;
using AsiTest.Business.Entities.Contact;
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

    public void CreateContact(Contact contact)
    {
        // Need to check if more than one email has more than one set as primary and correct it
        // We will just go with the first one as primary if any more are set
        if (contact.Emails.Count(e => e.IsPrimary) > 1)
        {
            foreach (var email in contact.Emails.Where(e => e.IsPrimary).Skip(1))
            {
                email.IsPrimary = false;
            }
        }
        
        _contactRepository.Create(contact); 
        _applicationContext.SaveChanges();
    }
    
    #endregion create
    
    #region read

    public Contact? GetContactById(long id)
    {
        return _contactRepository.FindById(id);
    }

    public IEnumerable<Contact> GetContacts()
    {
        return _contactRepository.FindAll().Include(c => c.Emails);
    }

    public IEnumerable<Contact> FindContacts(string name)
    {
        return _contactRepository.FindByCondition(c => c.Name.IndexOf(name, StringComparison.InvariantCultureIgnoreCase) > -1);
    }

    public IEnumerable<Contact> FindContacts(DateOnly startDate, DateOnly endDate)
    {
        return _contactRepository.FindByCondition(c => c.BirthDate >= startDate && c.BirthDate <= endDate);
    }

    #endregion read
    
    #region update
    
    public void UpdateContact(Contact contact)
    {
        // Need to check if more than one email has more than one set as primary and correct it
        // We will just go with the first one as primary if any more are set
        foreach (var email in contact.Emails.Where(e => e.IsPrimary).Skip(1))
        {
            email.IsPrimary = false;
        }
        
        _contactRepository.Update(contact); 
        _applicationContext.SaveChanges();
    }
    
    #endregion update
    
    #region delete

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