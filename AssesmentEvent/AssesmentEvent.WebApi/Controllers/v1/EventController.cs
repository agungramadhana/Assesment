using AssesmentEvent.Application;
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
            try
            {
                return Ok(await Mediator.Send(new GetEventQuery()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new BadRequestException(ex.Message);                
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetEventById(Guid Id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetEventByIdQuery
                {
                    Id = Id
                }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new BadRequestException(ex.Message);
            }   
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventCommand request)
        {
            try
            {
                if (!ModelState.IsValid) throw new BadRequestException("request invalid");

                return Ok(await Mediator.Send(request));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new BadRequestException(ex.Message);
            }
        }

        [HttpPost("order")]
        public async Task<IActionResult> CreateOrderEvent([FromBody] CreateOrderEventCommand request)
        {
            try
            {
                if(!ModelState.IsValid) throw new BadRequestException("request invalid");

                return Ok(await Mediator.Send(request));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new BadRequestException(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEvent([FromBody] UpdateEventCommand request)
        {
            try
            {
                if (!ModelState.IsValid) throw new BadRequestException("request invalid");

                return Ok(await Mediator.Send(request));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new BadRequestException(ex.Message);
            }
        }

        [HttpPut("update-category")]
        public async Task<IActionResult> UpdateEventCategory([FromBody] UpdateEventCategoryCommand request)
        {
            try
            {
                if (!ModelState.IsValid) throw new BadRequestException("request invalid");

                return Ok(await Mediator.Send(request));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new BadRequestException(ex.Message);
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteEventById(Guid Id)
        {
            try
            {
                return Ok(await Mediator.Send(new DeleteEventCommand
                {
                    Id = Id
                }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new BadRequestException(ex.Message);
            }
        }
    }
}
