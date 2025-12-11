namespace EventPlanner.Domain.Entities;

public class Registration
{
    public Guid Id { get; private set; }

    public Guid UserId { get; private set; }
    public Guid EventId { get; private set; }

    public User User { get; set; } = default!;
    public Event Event { get; set; } = default!;
    
    public DateTime RegisteredAt { get; private set; }

    public Registration(Guid id, Guid userId, Guid eventId)
    {
        Id = id;
        UserId = userId;
        EventId = eventId;
        RegisteredAt = DateTime.UtcNow;
    }
    private Registration()
    {
        
    }
}