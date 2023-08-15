using AsiTest.Business.Contexts;
using AsiTest.Business.Entities.Contact;

namespace AsiTest.Business.Repositories;

/// <summary>
/// Repository for CRUD operations on Email entities
/// </summary>
public class EmailRepository : RepositoryBase<Email>
{
    private readonly ApplicationContext _applicationContext;

    public EmailRepository(ApplicationContext applicationContext) : base(applicationContext)
    {
        _applicationContext = applicationContext;
    }
}