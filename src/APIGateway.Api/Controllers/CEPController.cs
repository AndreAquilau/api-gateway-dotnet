using APIGateway.Api.DTOs;
using APIGateway.Application.CEP.UseCases.ConsultarCep;
using APIGateway.Application.CEP.UseCases.ConsultarCepAsync;
using APIGateway.Application.Presenters.CEP;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace APIGateway.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CEPController : ControllerBase
{
    private readonly IMediator _mediator;

    public CEPController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("v1/cep/ConsultaAsync/{cep}")]
    public async Task<ActionResult<ResponseAsync?>> GetCEPAsync(string cep)
    {
        try
        {

            var result = await _mediator.Send(new ConsultarCepAsyncRequest(cep: cep));

            return Ok(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

            var error = new { error = true, message = ex.InnerException?.Message ?? nameof(GetCEPAsync) };

            return BadRequest(error);
        }

    }

    [HttpGet("v1/Consulta/{cep}")]
    public async Task<ActionResult<CEPPresenter?>> GetCEP(string cep)
    {
        try
        {
            CEPPresenter? result = await _mediator.Send(new ConsultarCepRequest(cep: cep));

            Console.WriteLine(result);

            await Task.Delay(10000);

            return Ok(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

            var error = new { error = true, message = ex.InnerException?.Message ?? nameof(GetCEP) };

            return BadRequest(error);
        }

    }
}
