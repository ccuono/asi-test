namespace AsiTest.Business.Entities.Contact;

/// <summary>
/// Email Entity
/// </summary>
public class Email
{
    public long Id { get; set; }
    public bool IsPrimary { get; set; }
    public string Address { get; set; } = default!;
}