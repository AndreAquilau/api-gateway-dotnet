using APIGateway.Api.DTOs;
using APIGateway.Application.CEP.UseCases.ConsultarCep;
using APIGateway.Application.CEP.UseCases.ConsultarCepAsync;
using APIGateway.Application.Presenters.CEP;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIGateway.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CEPController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _contextAccessor;

    public CEPController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
        _contextAccessor = httpContextAccessor;
    }

    [HttpGet("v1/cep/ConsultaAsync/{cep}")]
    public async Task<ActionResult<ResponseAsync?>> GetCEPAsync(string cep)
    {
        try
        {
            var transactionId = _contextAccessor?.HttpContext?.TraceIdentifier ?? Guid.NewGuid().ToString();

            Console.WriteLine($"TransactionId: {_contextAccessor?.HttpContext?.TraceIdentifier}");

            var result = await _mediator.Send(new ConsultarCepAsyncRequest(cep: cep, transactionId: transactionId) );

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
            var transactionId = _contextAccessor?.HttpContext?.TraceIdentifier ?? Guid.NewGuid().ToString(); 

            Console.WriteLine($"TransactionId: {_contextAccessor?.HttpContext?.TraceIdentifier}");

            CEPPresenter? result = await _mediator.Send(new ConsultarCepRequest(cep: cep, transactionId: transactionId));

            Console.WriteLine(result);

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
