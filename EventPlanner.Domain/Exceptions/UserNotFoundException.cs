using EventPlanner.Domain.ValueObjects;

namespace EventPlanner.Domain.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(EmailAddress email)
        : base($"The user with email {email.Value} was not found.")
    {
    }
}