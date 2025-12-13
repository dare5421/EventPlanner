using System;

namespace EventPlanner.Application.Features.Event.GetAllEvents;

public class EventSummaryDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Location { get; set; } = string.Empty;
}
