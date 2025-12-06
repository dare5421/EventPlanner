using EventPlanner.Domain.Exceptions;

namespace EventPlanner.Domain.Entities;

public class Event
{
    public Guid Id { get; private set; }

    // Properties
    public string Title { get; private set; }
    public DateTime Date { get; private set; }
    public int Capacity { get; private set; }

    public Event(Guid id, string title, DateTime date, int capacity)
    {
        Id = id;
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title cannot be empty.", nameof(title));
        }
        Title = title;
        Date = date;
        if (capacity < 0)
        {
            throw new ArgumentException("Capacity cannot be negative.", nameof(capacity));
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

    public void ReserveSeat()
    {
        if(!CanRegister())
        {
            throw new EventFullException(this.Id);
        }
        // Logic to reserve a seat (e.g., increment a counter or add a registration)
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
