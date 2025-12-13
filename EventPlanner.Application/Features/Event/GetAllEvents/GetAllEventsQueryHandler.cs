using System;
using EventPlanner.Domain.Repositories;
using MediatR;

namespace EventPlanner.Application.Features.Event.GetAllEvents;

public class GetAllEventsQueryHandler : IRequestHandler<GetAllEventsQuery, List<EventSummaryDto>>
{
    private readonly IEventRepository _eventRepository;
    public GetAllEventsQueryHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
    public async Task<List<EventSummaryDto>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
    {
        var events = await _eventRepository.GetAllAsync(cancellationToken);
        return events.Select(e => new EventSummaryDto
        {
            Id = e.Id,
            Title = e.Title,
            Date = e.Date,
            Location = e.Location
        }).ToList();
    }
}
