using APIGateway.Application.Presenters.CEP;
using APIGateway.Infrastructure.CEPService.Services;
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
    private readonly CEPService _CepService;
    private readonly ClientKafka _clientKafka;

    public ConsultarCepAsyncHandler(CEPService CepService, IMapper mapper, ClientKafka clientKafka)
    {
        _mapper = mapper;
        _CepService = CepService;
        _clientKafka = clientKafka;
    }

    public async Task<CEPPresenterAsync> Handle(ConsultarCepAsyncRequest request, CancellationToken cancellationToken)
    {
        var transactionId = await _clientKafka.BasicProducer("cep-topic.request", request.TransactionId, request);

        return new CEPPresenterAsync { transactionId = transactionId };
    }
}
