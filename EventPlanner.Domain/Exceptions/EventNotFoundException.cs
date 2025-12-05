namespace EventPlanner.Domain.Exceptions;

public class EventNotFoundException : Exception
{
    public EventNotFoundException(Guid eventId)
        : base($"The event with ID {eventId} was not found.")
    {        
    }
}