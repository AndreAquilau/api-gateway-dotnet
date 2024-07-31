using Microsoft.AspNetCore.Mvc;

namespace APIGateway.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExampleController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetCliente()
    {
        await Task.Run(() =>
        {

            Thread.Sleep(1000);

        });

        return Ok();
    }
}
