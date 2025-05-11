using AssesmentEvent.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssesmentEvent.WebApi.Controllers.v1
{
    [Authorize]
    public class EventController : BaseApiController
    {
    }
}
