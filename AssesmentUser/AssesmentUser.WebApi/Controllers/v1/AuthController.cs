using AssesmentUser.Application;
using AssesmentUser.Application.Features;
using AssesmentUser.Application.Features.Auth.Queries;
using Microsoft.AspNetCore.Mvc;

namespace AssesmentUser.WebApi.Controllers.v1
{
    public class AuthController : BaseApiController
    {
        private ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }

        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [HttpPost(template: "login")]
        public async Task<ActionResult> Login([FromBody] LoginQuery login)
        {
            try
            {
                return Ok(await Mediator.Send(login));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new BadRequestException(ex.Message);
            }
        }

        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [HttpPost(template: "register")]
        public async Task<ActionResult> register([FromBody] RegisterCommand request)
        {
            try
            {
                if (!ModelState.IsValid) throw new BadRequestException("Request invalid");

                return Ok(await Mediator.Send(request));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new BadRequestException(ex.Message);
            }
        }
    }
}
