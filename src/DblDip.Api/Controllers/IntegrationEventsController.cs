using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Api.Controllers
{
    [ApiController]
    [Route("api/integration-events")]
    public class IntegrationEventsController : Controller
    {
        [HttpGet("connect")]
        public async Task Connect(CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

            var response = Response;
            response.Headers.Add("Content-Type", "text/event-stream");

            /*            _integrationEventService.Subscribe(async e =>
                        {
                            var @event = JsonConvert.SerializeObject(e);

                            await response
                            .WriteAsync($"data: {@event}\r\r");

                            response.Body.Flush();

                        });*/

            await tcs.Task;

        }
    }
}
