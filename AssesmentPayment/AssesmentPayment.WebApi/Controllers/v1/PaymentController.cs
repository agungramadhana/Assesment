using AssesmentPayment.Application;
using AssesmentPayment.Application.Features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssesmentPayment.WebApi.Controllers.v1
{
    [Authorize]
    public class PaymentController : BaseApiController
    {
        [HttpGet("my-payment")]
        public async Task<IActionResult> GetMyPayment()
        {
            return Ok(await Mediator.Send(new GetMyPaymentQuery()));
        }

        [HttpPut]
        public async Task<IActionResult> Payment([FromBody] UpdatePaymentCommand request)
        {
            if (!ModelState.IsValid) throw new BadRequestException("request invalid");

            return Ok(await Mediator.Send(request));
        }
    }
}
