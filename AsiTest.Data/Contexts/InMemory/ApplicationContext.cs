using AsiTest.Business.Entities.Contact;
using Microsoft.EntityFrameworkCore;

namespace AsiTest.Business.Contexts.InMemory;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
    }
    
    public DbSet<Contact>? Contacts { get; set; }
    public DbSet<Email>? Emails { get; set; }
}