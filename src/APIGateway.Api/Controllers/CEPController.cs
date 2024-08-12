using APIGateway.Application.CEP.UseCases.ConsultarCep;
using APIGateway.Application.CEP.UseCases.ConsultarCepAsync;
using APIGateway.Application.Presenters;
using APIGateway.Application.Presenters.CEP;
using APIGateway.Infrastructure.Data;
using APIGateway.Infrastructure.Data.Services;
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
    private readonly PayloadService<ConsultarCepRequest, RequestDatabaseSettings> _requestPayloadService;
    private readonly PayloadService<CEPPresenter, ReponseDatabaseSettings> _responsePayloadService;

    public CEPController(
        IMediator mediator, 
        IHttpContextAccessor httpContextAccessor,
        PayloadService<ConsultarCepRequest, RequestDatabaseSettings> requestPayloadService,
        PayloadService<CEPPresenter, ReponseDatabaseSettings> responsePayloadService
        )
    {
        _mediator = mediator;
        _contextAccessor = httpContextAccessor;
        _requestPayloadService = requestPayloadService;
        _responsePayloadService = responsePayloadService;
    }

    [HttpGet("v1/cep/ConsultaAsync/{cep}")]
    public async Task<ActionResult<CEPPresenterAsync?>> GetCEPAsync(string cep)
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

    [HttpGet("v1/Consulta/Cep/{transactionId}")]
    public async Task<ActionResult<CEPPresenter?>> GetCEPByTransactionId(string transactionId)
    {
        var response = await _responsePayloadService.GetAsyncId(transactionId);

        if (response is null)
        {
            return NotFound();
        }

        return response.payload;
    }
}
