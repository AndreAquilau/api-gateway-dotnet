using APIGateway.Application.Presenters.CEP;
using APIGateway.Domain.CEP.Services;
using APIGateway.Infrastructure.CEPService.Services;
using APIGateway.Infrastructure.Data.Services;
using APIGateway.Infrastructure.Kafka;
using AutoMapper;
using MediatR;
using APIGateway.Infrastructure.Data.Models;
using System.Text.Json;
using APIGateway.Infrastructure.Data;

namespace APIGateway.Application.CEP.UseCases.ConsultarCep;
public class ConsultarCepHandler : IRequestHandler<ConsultarCepRequest, CEPPresenter>
{
    private readonly IMapper _mapper;
    private readonly ICEPService _CepService;
    private readonly ClientKafka _clientKafka;
    private readonly PayloadService<ConsultarCepRequest, RequestDatabaseSettings> _requestPayloadService;
    private readonly PayloadService<CEPPresenter, ReponseDatabaseSettings> _responsePayloadService;

    public ConsultarCepHandler(
        ICEPService CepService, 
        IMapper mapper, 
        ClientKafka clientKafka, 
        PayloadService<ConsultarCepRequest, RequestDatabaseSettings> requestPayloadService,
        PayloadService<CEPPresenter, ReponseDatabaseSettings> responsePayloadService
        )
    {
        _mapper = mapper;
        _CepService = CepService;
        _clientKafka = clientKafka;
        _requestPayloadService = requestPayloadService;
        _responsePayloadService = responsePayloadService;
    }

    public async Task<CEPPresenter> Handle(ConsultarCepRequest request, CancellationToken cancellationToken)
    {
        Console.WriteLine("Executando ConsultarCepHandler " + request.Cep);

        await _clientKafka.BasicProducer("cep-topic.request", request.TransactionId, request);

        await _requestPayloadService.CreateAsync(new GenericPayload<ConsultarCepRequest>() { payload = request, transactionId = request.TransactionId.ToString()});

        var response = await _CepService.ConsultarCepAsync(request.Cep);

        await _clientKafka.BasicProducer("cep-topic.response", request.TransactionId, response);

        var result = _mapper.Map<CEPPresenter>(response);

        await _responsePayloadService.CreateAsync(new GenericPayload<CEPPresenter>() { payload = result, transactionId = request.TransactionId });

        Console.WriteLine(response);

        return result;
    }
}
