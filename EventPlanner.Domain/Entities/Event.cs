using EventPlanner.Domain.Exceptions;

namespace EventPlanner.Domain.Entities;

public class Event
{
    public Guid Id { get; private set; }

    // Link to the user who created the event
    public Guid CreatorId { get; private set; }
    public string Location { get; private set; }

    // Properties
    public string Title { get; private set; }
    public DateTime Date { get; private set; }
    public int Capacity { get; private set; }

    public Event(Guid id,Guid creatorId, string location, string title, DateTime date, int capacity)
    {
        Id = id;
        CreatorId = creatorId;

        if (string.IsNullOrWhiteSpace(location))
        {
            throw new ArgumentException("Location cannot be empty.", nameof(location));
        }
        Location = location;

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title cannot be empty.", nameof(title));
        }
        Title = title;

        Date = date;

        if (capacity <= 0)
        {
            throw new ArgumentException("Capacity must be greater than zero.", nameof(capacity));
        }
        Capacity = capacity;
    }

    private Event()
    {
        
    }

    public ICollection<Registration> Registrations { get; } = new List<Registration>();

    public bool CanRegister()
    {
        return this.Registrations.Count < this.Capacity;
    }

    public void ReserveSeat(User participant)
    {
        if (!CanRegister())
            throw new EventFullException(this.Id);
        
        Registrations.Add(new Registration(Guid.NewGuid(), participant.Id, this.Id));
    }
    
    public void UpdateTitle(string newTitle)
    {
        if (string.IsNullOrWhiteSpace(newTitle))
        {
            throw new ArgumentException("Title cannot be empty.", nameof(newTitle));
        }
        Title = newTitle;
    }
    public void Reschedule(DateTime newDate)
    {
        Date = newDate;
    }
}
