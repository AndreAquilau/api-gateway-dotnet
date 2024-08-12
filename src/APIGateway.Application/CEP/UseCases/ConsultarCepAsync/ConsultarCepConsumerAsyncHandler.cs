using APIGateway.Application.Presenters.CEP;
using APIGateway.Domain.CEP.Services;
using APIGateway.Infrastructure.Data.Services;
using APIGateway.Infrastructure.Data;
using APIGateway.Infrastructure.Kafka;
using AutoMapper;
using MediatR;
using APIGateway.Infrastructure.Data.Models;

namespace APIGateway.Application.CEP.UseCases.ConsultarCepAsync;
public class ConsultarCepConsumerAsyncHandler : IRequestHandler<ConsultarCepAsyncConsumerRequest, CEPPresenter>
{
    private readonly IMapper _mapper;
    private readonly ClientKafka _clientKafka;
    private readonly ICEPService _CepService;
    private readonly PayloadService<CEPPresenter, ReponseDatabaseSettings> _responsePayloadService;

    public ConsultarCepConsumerAsyncHandler(
        ICEPService CepService, 
        IMapper mapper, 
        ClientKafka clientKafka,
        PayloadService<CEPPresenter, ReponseDatabaseSettings> responsePayloadService
        )
    {
        _mapper = mapper;
        _CepService = CepService;
        _clientKafka = clientKafka;
        _responsePayloadService = responsePayloadService;
    }

    public async Task<CEPPresenter> Handle(ConsultarCepAsyncConsumerRequest request, CancellationToken cancellationToken)
    {
        Console.WriteLine("Executando ConsultarCepConsumerAsyncHandler " + request.Cep);

        var response = await _CepService.ConsultarCepAsync(request.Cep);

        await _clientKafka.BasicProducer("cep-topic.response", request.TransactionId, response);
        
        var result = _mapper.Map<CEPPresenter>(response);

        await _responsePayloadService.CreateAsync(new GenericPayload<CEPPresenter>() { payload = result, transactionId = request.TransactionId });

        Console.WriteLine(response);

        return result;
    }
}