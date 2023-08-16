using AsiTest.Business.Entities.Contact;
using Microsoft.EntityFrameworkCore;

namespace AsiTest.Business.Contexts;

/// <summary>
/// The primary application context used
/// This is where we could override various methods like OnModelCreating
/// </summary>
public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
    }
    
    public DbSet<Contact>? Contacts { get; set; }
    public DbSet<Email>? Emails { get; set; }
}