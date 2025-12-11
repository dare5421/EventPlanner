namespace EventPlanner.Application.Features.Event.GetEventDetails;

public class EventDetailsDto
{
    public string Title { get; set; }
    public string Location { get; set; }
    public DateTime Date { get; set; }
    public int Capacity { get; set; }
    public int RegisteredCount { get; set; }
    public Guid CreatorId { get; set; }
}
