using AssesmentEvent.Application.Features;
using AssesmentEvent.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssesmentEvent.WebApi.Controllers.v1
{
    [Authorize]
    public class EventController : BaseApiController
    {
        private readonly ILogger<EventController> _logger;
        public EventController(ILogger<EventController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvent()
        {
            return Ok(await Mediator.Send(new GetEventQuery()));
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetEventById(Guid Id)
        {
            return Ok(await Mediator.Send(new GetEventByIdQuery
            {
                Id = Id
            }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventCommand request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpPost("order")]
        public async Task<IActionResult> CreateOrderEvent([FromBody] CreateOrderEventCommand request)
        {
            return Ok(await Mediator.Send(request));
        }
    }
}
