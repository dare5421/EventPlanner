namespace EventPlanner.Domain.Entities;

public class Event
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public DateTime Date { get; private set; }
    public int Capacity { get; private set; }

    // Read-only collection for navigation property
    public ICollection<Registration> Registrations { get; } = new List<Registration>();

    // Constructor for EF Core (Infrastructure concession)
    private Event() { }

    // Public Constructor (Domain Factory Method)
    public Event(Guid id, string title, DateTime date, int capacity)
    {
        Id = id;
        Date = date; // Date validation might go in the Application layer (e.g., date in future)

        // Argument Validation Checks
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));

        if (capacity <= 0) // Changed to check for zero or less
            throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be positive.");

        Title = title;
        Capacity = capacity;
    }

    // --- DOMAIN LOGIC (Behavior) ---

    public bool CanRegister()
    {
        // Counts the actual number of registrations to derive state
        return Registrations.Count < Capacity;
    }

    public void ReserveSeat()
    {
        if (!CanRegister())
        {
            // Use a custom exception defined in Domain/Exceptions
            throw new Exception("Event is at full capacity and cannot accept new registrations.");
        }
        // No explicit counter increment needed here because the Application layer 
        // will create a new Registration entity and save it to the database, 
        // which automatically updates the Registrations collection count on the next load.
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
        // Add rule: e.g., if (newDate < DateTime.Today) throw new Exception("Cannot reschedule to the past.");
        Date = newDate;
    }
}