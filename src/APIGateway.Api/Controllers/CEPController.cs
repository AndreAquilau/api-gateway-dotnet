using APIGateway.Application.CEP.UseCases.ConsultarCep;
using APIGateway.Application.DTOs;
using APIGateway.Application.DTOs.CEPDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace APIGateway.Api.Controllers;

[Route("api/v1")]
[ApiController]
public class CEPController : ControllerBase
{
    private readonly IMediator _mediator;

    public CEPController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("cep/{cep}")]
    public async Task<ActionResult<CEPDto?>> GetCEP(string cep)
    {
        try
        {
            CEPDto? result = null;

            var task = Task.Run(async () =>
            {
                result = await _mediator.Send(new ConsultarCepRequest(cep: cep));
                Thread.Sleep(1000);
            });

            task.Wait(10000);

            Console.WriteLine(result);

            return Ok(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

            var error = new { error = true, message = ex.InnerException?.Message ?? nameof(GetCEP)};

            return BadRequest(error);
        }

    }
}
