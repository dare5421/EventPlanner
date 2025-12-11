using System;
using MediatR;

namespace EventPlanner.Application.Features.Event.GetEventDetails;

public class GetEventByIdQuery:IRequest<EventDetailsDto>
{
    public Guid EventId { get; set; }
}
