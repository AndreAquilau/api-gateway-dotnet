using APIGateway.Application.DTOs.CEPDtos;
using APIGateway.Domain.CEP.ObjectValues;
using APIGateway.Infrastructure.CEPService.Services;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIGateway.Application.CEP.UseCases.ConsultarCep;
public class ConsultarCepHandler : IRequestHandler<ConsultarCepRequest, CEPDto>
{
    private readonly IMapper _mapper;
    private readonly CEPService _CepService;

    public ConsultarCepHandler(CEPService CepService, IMapper mapper)
    {
        _mapper = mapper;
        _CepService = CepService;
    }

    public async Task<CEPDto> Handle(ConsultarCepRequest request, CancellationToken cancellationToken)
    {
        var response = await _CepService.ConsultarCepAsync(request.Cep);

        Console.WriteLine(response);

        return _mapper.Map<CEPDto>(response);
    }
}
