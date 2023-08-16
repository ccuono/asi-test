// Very crude console application demonstrating library usage

using AsiTest.Business.Contexts;
using AsiTest.Business.Data.Testing;
using AsiTest.Business.Entities.Contact;
using AsiTest.Business.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

Console.WriteLine("Welcome to the ASI Test Interactive Console");

var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
var builtConfiguration = configurationBuilder.Build();

var databaseName = builtConfiguration["database:name"];

var dbContextOptionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
dbContextOptionsBuilder.UseInMemoryDatabase(databaseName ?? "AsiTestDb");

SeedDatabase();

if (args.Length <= 0)
{
    while (true)
    {
        PrintMainMenu();
        if (int.TryParse(Console.ReadLine(), out var selectedValue))
        {
            Selection(selectedValue);
        }
    }
}
else
{
    // Args could be used to augment this, right now let's just print out all the contacts that were randomly generated
    Selection(1);
    Environment.Exit(0);
}

void SeedDatabase()
{
    using var applicationContext = new ApplicationContext(dbContextOptionsBuilder.Options);
    var contactService = new ContactService(applicationContext);

    var applicationDataSeeder = new ApplicationDataSeeder(contactService);

    var contactGenerationAmountConfig = builtConfiguration["database:contactGenerationAmount"];
    var emailMaxConfig = builtConfiguration["database:emailMax"];

    if (int.TryParse(contactGenerationAmountConfig, out var contactGenerationAmount) &&
        int.TryParse(emailMaxConfig, out var emailMax))
    {
        applicationDataSeeder.InitializeData(contactGenerationAmount, emailMax);
    }
    else
    {
        applicationDataSeeder.InitializeData();
    }
}

void Selection(int selection)
{
    switch (selection)
    {
        case 1:
            PrintAllContacts();
            break;
        case 2:
            PrintContactNameSearch();
            break;
        case 3:
            PrintContactBirthDateSearch();
            break;
        case 4:
            PrintCreateContact();
            break;
        case 5:
            PrintRandomGeneration();
            break;
        case 6:
            PrintEditContact();
            break;
        case 7:
            PrintDeleteContact();
            break;
        case 8:
            Console.WriteLine("Quitting...");
            Environment.Exit(0);
            break;
        default:
            PrintMainMenu();
            break;
    }
}

void PrintMainMenu()
{
    Console.BackgroundColor = ConsoleColor.Black;
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("Please select from the following menu options:");
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"[1] Show all Contacts within the {databaseName} database");
    Console.WriteLine("[2] Search for Contacts by Name");
    Console.WriteLine("[3] Search for Contacts who have a BirthDate between certain dates");
    Console.WriteLine("[4] Add a new Contact record");
    Console.WriteLine($"[5] Add a number of random Contacts to the {databaseName} database");
    Console.WriteLine("[6] Update an existing Contact");
    Console.WriteLine("[7] Delete a contact by their Id");
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("[8] Quit");
    Console.ResetColor();
}

void PrintContact(Contact contact)
{
    Console.ForegroundColor = ConsoleColor.Black;
    Console.BackgroundColor = ConsoleColor.Green;

    Console.WriteLine($"\tId: {contact.Id}");
    Console.WriteLine($"\tName: {contact.Name}");
    Console.WriteLine($"\tBirthDate: {contact.BirthDate}");
    Console.WriteLine("\tEmails:");

    PrintEmails(contact.Emails);

    Console.ResetColor();
}

void PrintEmails(IEnumerable<Email> emails)
{
    Console.ForegroundColor = ConsoleColor.Black;
    Console.BackgroundColor = ConsoleColor.Green;
    foreach (var email in emails)
    {
        Console.WriteLine($"\t\tId: {email.Id}");
        Console.WriteLine($"\t\tAddress: {email.Address}");
        Console.WriteLine($"\t\tIsPrimary: {email.IsPrimary}");
    }
}

void PrintContacts(IList<Contact> contacts)
{
    if (contacts.Any())
    {
        Console.WriteLine($"There are {contacts.Count} contacts in the database");

        for (var i = 0; i < contacts.Count; i++)
        {
            var contact = contacts[i];
            Console.WriteLine($"Contact {i + 1}:");
            PrintContact(contact);
            Console.WriteLine(string.Empty);
        }
    }
    else
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("There are no results.");
    }

    Console.ResetColor();
}

void PrintAllContacts()
{
    
    using var applicationContext = new ApplicationContext(dbContextOptionsBuilder.Options);
    var contactService = new ContactService(applicationContext);
    
    var contacts = contactService.GetContacts().ToList();

    PrintContacts(contacts);
}

void PrintContactNameSearch()
{
    using var applicationContext = new ApplicationContext(dbContextOptionsBuilder.Options);
    var contactService = new ContactService(applicationContext);
    
    Console.BackgroundColor = ConsoleColor.Black;
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("Please enter search phrase for Contact Name:");
    var searchPhrase = Console.ReadLine();
    while (string.IsNullOrWhiteSpace(searchPhrase))
    {
        Console.WriteLine("Search phrase should not be empty.");
        Console.WriteLine("Please enter search phrase for Contact Name:");
        searchPhrase = Console.ReadLine();
    }

    Console.ForegroundColor = ConsoleColor.Black;
    Console.BackgroundColor = ConsoleColor.Green;

    var contactResults = contactService.FindContacts(searchPhrase).ToList();

    PrintContacts(contactResults);

    Console.ResetColor();
}

void PrintContactBirthDateSearch()
{
    using var applicationContext = new ApplicationContext(dbContextOptionsBuilder.Options);
    var contactService = new ContactService(applicationContext);

    Console.BackgroundColor = ConsoleColor.Black;
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("Please enter a start date for BirthDate search:");
    DateOnly startDate;
    while (!DateOnly.TryParse(Console.ReadLine(), out startDate))
    {
        Console.WriteLine(
            $"Start date should be in ISO 8601 or other appropriate format ({DateOnly.FromDateTime(DateTime.UnixEpoch).ToString("O")}):");
    }

    Console.WriteLine("Please enter the ISO 8601 end date for BirthDate search:");
    DateOnly endDate;
    while (!DateOnly.TryParse(Console.ReadLine(), out endDate))
    {
        Console.WriteLine(
            $"End date must be in ISO 8601 or other appropriate format ({DateOnly.FromDateTime(DateTime.UnixEpoch).ToString("O")}):");
    }

    var contactResults = contactService.FindContacts(startDate, endDate).ToList();

    PrintContacts(contactResults);

    Console.ResetColor();
}

void PrintCreateContact(Contact? contactToCreate = null)
{
    using var applicationContext = new ApplicationContext(dbContextOptionsBuilder.Options);
    var contactService = new ContactService(applicationContext);
    
    var contact = contactToCreate ?? new Contact();

    Console.BackgroundColor = ConsoleColor.Black;
    Console.ForegroundColor = ConsoleColor.Yellow;

    Console.WriteLine("Please enter a Contact Name:");
    contact.Name = Console.ReadLine() ?? string.Empty;

    while (string.IsNullOrWhiteSpace(contact.Name))
    {
        Console.WriteLine("Contact Name should not be blank.");
        Console.WriteLine("Please enter a Contact Name:");
    }

    Console.WriteLine("Please enter a BirthDate (blank for none):");
    var birthDateString = Console.ReadLine();

    if (!string.IsNullOrWhiteSpace(birthDateString) && DateOnly.TryParse(birthDateString, out var birthDate))
    {
        contact.BirthDate = birthDate;
    }
    else
    {
        while (!string.IsNullOrWhiteSpace(birthDateString) && !DateOnly.TryParse(birthDateString, out birthDate))
        {
            Console.WriteLine(
                $"BirthDate should be in ISO 8601 or other appropriate format ({DateOnly.FromDateTime(DateTime.UnixEpoch).ToString("O")}). Use blank for none:");
        }

        if (string.IsNullOrWhiteSpace(birthDateString))
        {
            contact.BirthDate = null;
        }
    }

    if (contactToCreate == null)
    {
        contact.Emails = new List<Email>();
    }
    else
    {
        PrintEmails(contact.Emails);
        Console.WriteLine(
            "Would you like to remove any of these emails so that you can optionally add new ones in their place? [y/N]");
        while (Console.ReadLine()!.Trim().StartsWith("y", StringComparison.InvariantCultureIgnoreCase))
        {
            Console.WriteLine("Please enter the Id of the email you'd like to delete:");
            int emailId;
            while (!int.TryParse(Console.ReadLine(), out emailId))
            {
                Console.WriteLine("Id needs to be a whole number.");
                Console.WriteLine("Please enter the Id of the email you'd like to delete:");
            }

            var email = contact.Emails.FirstOrDefault(e => e.Id == emailId);

            if (email != null)
            {
                contact.Emails.Remove(email);
            }

            PrintEmails(contact.Emails);
            Console.WriteLine(
                "Would you like to remove any of these emails so that you can optionally add new ones in their place? [y/N]");
        }
    }

    Console.WriteLine("Would you like to add an Email to this contact? [Y/n]");

    while (!Console.ReadLine()!.Trim().StartsWith("n", StringComparison.InvariantCultureIgnoreCase))
    {
        Console.WriteLine("Please enter an email address:");
        var emailAddress = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(emailAddress))
        {
            Console.WriteLine("Email address should not be blank.");
            Console.WriteLine("Please enter an email address:");
            emailAddress = Console.ReadLine();
        }

        bool isPrimary;
        Console.WriteLine("Is this the primary email address? [true/false]");
        while (!bool.TryParse(Console.ReadLine(), out isPrimary))
        {
            Console.WriteLine("Please enter `true` or `false`.");
            Console.WriteLine("Is this the primary email address? [true/false]");
        }

        contact.Emails.Add(new Email
        {
            Address = emailAddress,
            IsPrimary = isPrimary
        });
        Console.WriteLine("Would you like to add another Email to this contact? [Y/n]");
    }

    if (contactToCreate == null)
    {
        contactService.CreateContact(contact);
    }
    else
    {
        contactService.UpdateContact(contact);
    }

    PrintContact(contact);

    Console.ResetColor();
}

void PrintRandomGeneration()
{
    using var applicationContext = new ApplicationContext(dbContextOptionsBuilder.Options);
    var contactService = new ContactService(applicationContext);

    var applicationDataSeeder = new ApplicationDataSeeder(contactService);
    
    Console.BackgroundColor = ConsoleColor.Black;
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("Please enter the number of Contacts that you'd like to generate:");

    int contactsNumber;

    while (!int.TryParse(Console.ReadLine(), out contactsNumber))
    {
        Console.WriteLine("Please enter a whole number.");
        Console.WriteLine("Please enter the number of Contacts that you'd like to generate:");
    }

    Console.WriteLine("Please enter the maximal amount of emails to generate for each contact:");
    int maxEmailsNumber;

    while (!int.TryParse(Console.ReadLine(), out maxEmailsNumber))
    {
        Console.WriteLine("Please enter a whole number.");
        Console.WriteLine("Please enter the maximal amount of emails to generate for each contact:");
    }

    applicationDataSeeder.InitializeData(contactsNumber, maxEmailsNumber);

    PrintAllContacts();

    Console.ResetColor();
}

void PrintEditContact()
{
    PrintAllContacts();

    using var applicationContext = new ApplicationContext(dbContextOptionsBuilder.Options);
    var contactService = new ContactService(applicationContext);

    Console.BackgroundColor = ConsoleColor.Black;
    Console.ForegroundColor = ConsoleColor.Yellow;

    Console.WriteLine("Please enter the Id of the Contact you'd like to edit:");

    long contactId;
    while (!long.TryParse(Console.ReadLine(), out contactId))
    {
        Console.WriteLine("Please enter a valid whole number.");
        Console.WriteLine("Please enter the Id of the Contact you'd like to edit:");
    }

    var contactToEdit = contactService.GetContactById(contactId);

    if (contactToEdit == null)
    {
        // Return out, it's going to be way faster to re-prompt
        // We could also utilize a goto here, but... just start over
        Console.WriteLine("Please enter a valid Id.");
        Console.WriteLine("Try again with a valid Id.");
        return;
    }

    Console.WriteLine("This is the contact you will be editing:");

    PrintContact(contactToEdit);

    Console.WriteLine("Do you want to proceed with altering this contact? [y/N]");

    if (Console.ReadLine()!.Trim().StartsWith("y", StringComparison.InvariantCultureIgnoreCase))
    {
        PrintCreateContact(contactToEdit);
    }

    Console.ResetColor();
}

void PrintDeleteContact()
{
    PrintAllContacts();
    
    using var applicationContext = new ApplicationContext(dbContextOptionsBuilder.Options);
    var contactService = new ContactService(applicationContext);

    Console.BackgroundColor = ConsoleColor.Black;
    Console.ForegroundColor = ConsoleColor.Yellow;
    
    Console.WriteLine("Please enter the Id of the Contact you'd like to delete:");

    int contactId;

    while (!int.TryParse(Console.ReadLine(), out contactId))
    {
        Console.WriteLine("Please enter a whole number.");
        Console.WriteLine("Please enter the Id of the contact you'd like to delete:");
    }
    
    contactService.DeleteContactById(contactId);
    
    PrintAllContacts();
    
    Console.WriteLine($"The Contact with Id {contactId} was deleted.");
    
    Console.ResetColor();
}