using EventPlanner.Application.Features.Event.CreateEvent;
using EventPlanner.Application.Features.Event.GetEventDetails;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly ISender _mediator;

        public EventController(ISender mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventCommand command)
        {
            Guid eventId = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreateEvent), new { id = eventId }, eventId);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetEventById([FromRoute] Guid id)
        {
            var query = new GetEventByIdQuery { EventId = id };
            var eventDetails = await _mediator.Send(query);
            return Ok(eventDetails);
        }
    }
}
