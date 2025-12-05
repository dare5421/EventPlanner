namespace EventPlanner.Domain.Exceptions;

public class EventFullException : Exception
{
    public EventFullException( Guid eventId )
        : base($"The event with ID {eventId} is full and cannot accept more registrations.")
    {
    }
}