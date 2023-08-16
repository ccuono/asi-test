using AsiTest.Business.Entities.Contact;

namespace AsiTest.Business.Extensions;

public static class CorrectionExtensions
{
    /// <summary>
    /// Method to enforce "IsPrimary" rule where one and only one email can have IsPrimary=true
    /// </summary>
    /// <param name="emails"></param>
    public static void CorrectEmails(this ICollection<Email> emails)
    {
        // Need to check if more than one email has more than one set as primary and correct it
        // We will just go with the first one as primary if any more are set
        foreach (var email in emails.Where(e => e.IsPrimary).Skip(1))
        {
            email.IsPrimary = false;
        }

        if (emails.All(e => !e.IsPrimary))
        {
            emails.First().IsPrimary = true;
        }
    }
}