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

    public ConsultarCepHandler(CEPService CepService, IMapper mapper)
    {
        _mapper = mapper;
        _CepService = CepService;
    }

    public async Task<CEPPresenter> Handle(ConsultarCepRequest request, CancellationToken cancellationToken)
    {
        var response = await _CepService.ConsultarCepAsync(request.Cep);

        Console.WriteLine(response);

        return _mapper.Map<CEPPresenter>(response);
    }
}
