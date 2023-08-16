using AsiTest.Business.Entities.Contact;

namespace AsiTest.Business.Services;

public interface IContactService
{
    /// <summary>
    /// Creates a Contact with supplied contact object
    /// </summary>
    /// <param name="contact"></param>
    void CreateContact(Contact contact);

    /// <summary>
    /// Returns contact with supplied id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Contact? GetContactById(long id);

    /// <summary>
    /// Returns contact with supplied id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Email? GetEmailById(long id);

    /// <summary>
    /// Returns all contacts
    /// </summary>
    /// <returns></returns>
    IEnumerable<Contact> GetContacts();

    /// <summary>
    /// Returns contacts where Name contains supplied name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    IEnumerable<Contact> FindContacts(string name);

    /// <summary>
    /// Returns contacts whose BirthDate are between the supplied startDate and endDate
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    IEnumerable<Contact> FindContacts(DateOnly startDate, DateOnly endDate);

    /// <summary>
    /// Updates a contact with the supplied contact object
    /// </summary>
    /// <param name="contact"></param>
    void UpdateContact(Contact contact);

    /// <summary>
    /// Deletes a Contact with supplied id
    /// </summary>
    /// <param name="id"></param>
    void DeleteContactById(long id);

    /// <summary>
    /// Deletes an Email with supplied id
    /// </summary>
    /// <param name="id"></param>
    void DeleteEmailById(long id);
}