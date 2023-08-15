using AsiTest.Business.Entities.Contact;
using AsiTest.Business.Services;

namespace AsiTest.Business.Data;

public class ApplicationDataSeeder
{
    private readonly ContactService _contactService;

    public ApplicationDataSeeder(ContactService contactService)
    {
        _contactService = contactService;
    }
    
    public void InitializeData(int contactGenerationAmount = 25, int emailMax = 5)
    {
        var random = new Random();
        var startBirthDate = new DateTime(1945, 1, 1);
        var range = (DateTime.Today - startBirthDate).Days;
        
        for (var i = 0; i < contactGenerationAmount; i++)
        {
            List<Email> emails = new List<Email>();
            for (var j = 0; j < emailMax; j++)
            {
                emails.Add(new Email
                {
                    Address = $"{random.Next(100)}@{random.Next(1000)}.com",
                    IsPrimary = random.Next(1000) % 2 == 0
                });
            }

            var newContact = new Contact
            {
                BirthDate = DateOnly.FromDateTime(startBirthDate.AddDays(random.Next(range))),
                Emails = emails,
                Name = $"{random.Next(50)} Random {random.Next(100)}"
            };

            _contactService.CreateContact(newContact);
        }
    }
}