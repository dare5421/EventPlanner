using EventPlanner.Application.Features.Event.CreateEvent;
using EventPlanner.Application.Features.Event.GetAllEvents;
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

        [Authorize] // 🟢 FIX 1: Re-enable security now that authentication works
        [HttpPost("create")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)] // 🟢 FIX 2: Re-enable response type documentation
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]         // 🟢 FIX 2: Re-enable response type documentation
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventCommand command)
        {
            Guid eventId = await _mediator.Send(command);

            // 🟢 FIX 3: Point to the GET action so the response includes a Location header
            return CreatedAtAction(nameof(GetEventById), new { id = eventId }, eventId);
        }

        [HttpGet("{id:guid}", Name = "GetEventById")] // 🟢 FIX 4: Add Name property for CreateAtAction linking
        public async Task<IActionResult> GetEventById([FromRoute] Guid id)
        {
            var query = new GetEventByIdQuery { EventId = id };
            var eventDetails = await _mediator.Send(query);

            // Assuming the handler returns null on not found
            if (eventDetails == null)
            {
                return NotFound();
            }

            return Ok(eventDetails);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var query = new GetAllEventsQuery();
            var events = await _mediator.Send(query);
            return Ok(events);
        }
    }
}