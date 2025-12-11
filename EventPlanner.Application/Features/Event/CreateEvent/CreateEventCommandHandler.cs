using EventPlanner.Application.Exceptions;
using EventPlanner.Application.Interfaces;
using EventPlanner.Domain.Repositories;
using MediatR;

namespace EventPlanner.Application.Features.Event.CreateEvent;
public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
{
    private readonly IEventRepository _eventRepository;
    private readonly ICurrentUserService _currentUserService;

    public CreateEventCommandHandler(IEventRepository eventRepository, ICurrentUserService currentUserService)
    {
        _eventRepository = eventRepository;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        // 1. Get the CreatorId securely from the service
        var creatorId = _currentUserService.GetCurrentUserId();

        if (creatorId == null || creatorId == Guid.Empty)
            throw new UnauthorizedException("User must be authenticated to create an event.");

        // 2. Pass the secure ID to the Domain Entity              
        var newEvent = new Domain.Entities.Event(
            Guid.NewGuid(),
            creatorId.Value, // use the secure ID
            request.Location,
            request.Title,
            request.Date,
            request.Capacity
        );

        await _eventRepository.AddAsync(newEvent, cancellationToken);
        return newEvent.Id;
    }
}