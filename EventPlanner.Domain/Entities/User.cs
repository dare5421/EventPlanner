using EventPlanner.Domain.ValueObjects;

namespace EventPlanner.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    // Value Objects
    public EmailAddress Email { get; private set; } 
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    // this is set by Infrastructure, but stored here
    public string PasswordHash { get; private set; }

    private User()
    {

    }
    public User(Guid id, EmailAddress email, string firstName, string lastName)
    {
        Id = id;
        Email = email;
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new ArgumentException("First name cannot be empty.", nameof(firstName));
        }
        FirstName = firstName;
        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new ArgumentException("Last name cannot be empty.", nameof(lastName));
        }
        LastName = lastName;

    }

    //Navigation Properties
    public ICollection<Registration> Registrations { get; } = new List<Registration>();
    
    public void SetPasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
    }
}