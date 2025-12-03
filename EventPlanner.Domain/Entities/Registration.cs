namespace EventPlanner.Domain.Entities;

public class Registration
{
    public Guid UserId { get; set; }
    public Guid EventId { get; set; }
    public User User { get; set; } = default!;
    public Event Event { get; set; } = default!;
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
}