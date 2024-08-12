using APIGateway.Application.Presenters.CEP;
using APIGateway.Infrastructure.Kafka;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace APIGateway.Application.CEP.UseCases.ConsultarCepAsync;
public class ConsultarCepAsyncHandler : IRequestHandler<ConsultarCepAsyncRequest, CEPPresenterAsync>
{
    private readonly IMapper _mapper;
    private readonly ClientKafka _clientKafka;

    public ConsultarCepAsyncHandler(IMapper mapper, ClientKafka clientKafka)
    {
        _mapper = mapper;
        _clientKafka = clientKafka;
    }

    public async Task<CEPPresenterAsync> Handle(ConsultarCepAsyncRequest request, CancellationToken cancellationToken)
    {
        Console.WriteLine("Executando ConsultarCepAsyncHandler " + request.Cep);

        var transactionId = await _clientKafka.BasicProducer("cep-topic.request", request.TransactionId, request);

        return new CEPPresenterAsync { TransactionId = transactionId };
    }
}
