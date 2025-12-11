using System;
using EventPlanner.Domain.Exceptions;
using EventPlanner.Domain.Repositories;
using MediatR;

namespace EventPlanner.Application.Features.Event.GetEventDetails;

public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, EventDetailsDto>
{
    private readonly IEventRepository _eventRepository;
    public GetEventByIdQueryHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<EventDetailsDto> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
    {
        var currentEvent = await _eventRepository.GetByIdAsync(request.EventId, cancellationToken);
        if (currentEvent is null)
        {
            throw new EventNotFoundException(request.EventId);
        }
        return new EventDetailsDto
        {
            Title = currentEvent.Title,
            Location = currentEvent.Location,
            Date = currentEvent.Date,
            Capacity = currentEvent.Capacity,
            RegisteredCount = currentEvent.Registrations.Count,
            CreatorId = currentEvent.CreatorId
        };

    }
}
