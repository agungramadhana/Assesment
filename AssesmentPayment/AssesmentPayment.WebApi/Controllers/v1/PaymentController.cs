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
        private readonly ILogger<PaymentController> _logger;
        public PaymentController(ILogger<PaymentController> logger)
        {
            _logger = logger;
        }

        [HttpGet("my-payment")]
        public async Task<IActionResult> GetMyPayment()
        {
            try
            {
                return Ok(await Mediator.Send(new GetMyPaymentQuery()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new BadRequestException(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Payment([FromBody] UpdatePaymentCommand request)
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
    }
}
