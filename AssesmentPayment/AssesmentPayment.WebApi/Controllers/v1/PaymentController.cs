using AssesmentPayment.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssesmentPayment.WebApi.Controllers.v1
{
    [Authorize]
    public class PaymentController : BaseApiController
    {
    }
}
