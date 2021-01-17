using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/version")]
    public class VersionController
    {
        [HttpGet(Name = "GetVersion")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public ActionResult<string> GetAssemblyVersion()
            => GetType().Assembly.GetName().Version.ToString();

    }
}
