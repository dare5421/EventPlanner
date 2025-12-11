using MediatR;

namespace EventPlanner.Application.Features.Event.CreateEvent;

public record CreateEventCommand : IRequest<Guid>
{
    public string Location { get; init; }
    public string Title { get; init; }
    public DateTime Date { get; init; }
    public int Capacity { get; init; }
}
