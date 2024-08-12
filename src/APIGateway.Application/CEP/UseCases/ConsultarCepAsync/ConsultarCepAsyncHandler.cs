using APIGateway.Application.CEP.UseCases.ConsultarCep;
using APIGateway.Application.Presenters.CEP;
using APIGateway.Infrastructure.Data.Services;
using APIGateway.Infrastructure.Data;
using APIGateway.Infrastructure.Kafka;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using APIGateway.Infrastructure.Data.Models;

namespace APIGateway.Application.CEP.UseCases.ConsultarCepAsync;
public class ConsultarCepAsyncHandler : IRequestHandler<ConsultarCepAsyncRequest, CEPPresenterAsync>
{
    private readonly IMapper _mapper;
    private readonly ClientKafka _clientKafka;
    private readonly PayloadService<ConsultarCepAsyncRequest, RequestDatabaseSettings> _requestPayloadService;

    public ConsultarCepAsyncHandler(
        IMapper mapper, 
        ClientKafka clientKafka,
         PayloadService<ConsultarCepAsyncRequest, RequestDatabaseSettings> requestPayloadService
        )
    {
        _mapper = mapper;
        _clientKafka = clientKafka;
        _requestPayloadService = requestPayloadService;
    }

    public async Task<CEPPresenterAsync> Handle(ConsultarCepAsyncRequest request, CancellationToken cancellationToken)
    {
        Console.WriteLine("Executando ConsultarCepAsyncHandler " + request.Cep);

        var transaction = await _clientKafka.BasicProducer("cep-topic.request", request.TransactionId, request);

        await _requestPayloadService.CreateAsync(new GenericPayload<ConsultarCepAsyncRequest>() { payload = request, transactionId = request.TransactionId.ToString() });

        return new CEPPresenterAsync { TransactionId = transaction.Key, Topic = transaction.Topic, Offset = (int)transaction.Offset, CreatedAtUtc = transaction.Timestamp.UtcDateTime };
    }
}
