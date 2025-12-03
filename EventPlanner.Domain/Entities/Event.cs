using System.Runtime.InteropServices;

namespace EventPlanner.Domain.Entities;

public class Event
{
    public Guid Id { get; private set; }

    // Properties
    public string Title { get; private set; }
    public DateTime Date { get; private set; }
    public int Capacity { get; private set; }

    // STATE/BEHAVIOR TRACKING (Crucial!)

}
