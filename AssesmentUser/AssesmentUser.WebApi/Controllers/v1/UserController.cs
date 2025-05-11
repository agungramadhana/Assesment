using AssesmentUser.Application.Features;
using AssesmentUser.Application.Features.User.Commands;
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
            return Ok(await Mediator.Send(new GetListUserQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            return Ok(await Mediator.Send(new GetUserByIdQuery
            {
                Id = id
            }));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserQuery request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteUserQuery
            {
                Id = id
            }));
        }
    }
}
