using AsiTest.Business.Contexts;
using AsiTest.Business.Entities.Contact;

namespace AsiTest.Business.Repositories;

/// <summary>
/// Repository for CRUD operations on Contact entities
/// </summary>
public class ContactRepository : RepositoryBase<Contact>
{
    private readonly ApplicationContext _applicationContext;

    public ContactRepository(ApplicationContext applicationContext) : base(applicationContext)
    {
        _applicationContext = applicationContext;
    }
}