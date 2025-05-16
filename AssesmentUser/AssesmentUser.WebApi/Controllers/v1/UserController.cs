using AssesmentUser.Application;
using AssesmentUser.Application.Features;
using AssesmentUser.Application.Features.User.Commands;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssesmentUser.WebApi.Controllers.v1
{
    [Authorize]
    public class UserController : BaseApiController
    {
        private ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetListUser()
        {
            try
            {
                return Ok(await Mediator.Send(new GetListUserQuery()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new BadRequestException(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetUserByIdQuery
                {
                    Id = id
                }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new BadRequestException(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserQuery request)
        {
            try
            {
                return Ok(await Mediator.Send(request));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new BadRequestException(ex.Message);
            }
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                return Ok(await Mediator.Send(new DeleteUserQuery
                {
                    Id = id
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
