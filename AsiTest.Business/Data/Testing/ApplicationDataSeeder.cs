using AsiTest.Business.Entities.Contact;
using AsiTest.Business.Services;

namespace AsiTest.Business.Data.Testing;

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
                if (random.Next(1000) % 2 == 0)
                {
                    emails.Add(new Email
                    {
                        Address =
                            $"{NameData.GeneralNames[random.Next(NameData.GeneralNames.Length)]}@{NameData.Domains[random.Next(NameData.Domains.Length)]}{NameData.Tlds[random.Next(NameData.Tlds.Length)]}",
                        IsPrimary = random.Next(1000) % 2 == 0
                    });
                }
            }

            var newContact = new Contact
            {
                BirthDate = DateOnly.FromDateTime(startBirthDate.AddDays(random.Next(range))),
                Emails = emails,
                Name = $"{NameData.GeneralNames[random.Next(NameData.GeneralNames.Length)]} {NameData.GeneralNames[random.Next(NameData.GeneralNames.Length)]}"
            };

            _contactService.CreateContact(newContact);
        }
    }
}