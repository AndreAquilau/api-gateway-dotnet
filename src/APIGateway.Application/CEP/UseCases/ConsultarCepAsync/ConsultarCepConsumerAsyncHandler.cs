using APIGateway.Application.Presenters.CEP;
using APIGateway.Domain.CEP.Services;
using APIGateway.Infrastructure.Kafka;
using AutoMapper;
using MediatR;

namespace APIGateway.Application.CEP.UseCases.ConsultarCepAsync;
public class ConsultarCepConsumerAsyncHandler : IRequestHandler<ConsultarCepAsyncConsumerRequest, CEPPresenter>
{
    private readonly IMapper _mapper;
    private readonly ClientKafka _clientKafka;
    private readonly ICEPService _CepService;

    public ConsultarCepConsumerAsyncHandler(ICEPService CepService, IMapper mapper, ClientKafka clientKafka)
    {
        _mapper = mapper;
        _CepService = CepService;
        _clientKafka = clientKafka;
    }

    public async Task<CEPPresenter> Handle(ConsultarCepAsyncConsumerRequest request, CancellationToken cancellationToken)
    {
        Console.WriteLine("Executando ConsultarCepConsumerAsyncHandler " + request.Cep);

        var response = await _CepService.ConsultarCepAsync(request.Cep);

        await _clientKafka.BasicProducer("cep-topic.response", request.TransactionId, response);

        Console.WriteLine(response);

        return _mapper.Map<CEPPresenter>(response);
    }
}