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

namespace APIGateway.Application.CEP.UseCases.ConsultarCep;
public class ConsultarCepHandler : IRequestHandler<ConsultarCepRequest, CEPPresenter>
{
    private readonly IMapper _mapper;
    private readonly CEPService _CepService;
    private readonly ClientKafka _clientKafka;

    public ConsultarCepHandler(CEPService CepService, IMapper mapper, ClientKafka clientKafka)
    {
        _mapper = mapper;
        _CepService = CepService;
        _clientKafka = clientKafka;
    }

    public async Task<CEPPresenter> Handle(ConsultarCepRequest request, CancellationToken cancellationToken)
    {
        await _clientKafka.BasicProducer("cep-topic.request", request.TransactionId, request);

        var response = await _CepService.ConsultarCepAsync(request.Cep);

        await _clientKafka.BasicProducer("cep-topic.response", request.TransactionId, response);

        Console.WriteLine(response);

        return _mapper.Map<CEPPresenter>(response);
    }
}
