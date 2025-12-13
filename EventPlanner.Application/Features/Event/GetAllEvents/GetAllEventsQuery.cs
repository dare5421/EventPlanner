using MediatR;

namespace EventPlanner.Application.Features.Event.GetAllEvents;

public class GetAllEventsQuery : IRequest<List<EventSummaryDto>>
{

}
