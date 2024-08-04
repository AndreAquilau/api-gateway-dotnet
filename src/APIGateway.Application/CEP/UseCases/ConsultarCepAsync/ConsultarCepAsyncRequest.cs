using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIGateway.Application.Presenters.CEP;
using APIGateway.Domain.CEP.ObjectValues;
using MediatR;

namespace APIGateway.Application.CEP.UseCases.ConsultarCepAsync;
public class ConsultarCepAsyncRequest : IRequest<CEPPresenterAsync>
{
    public string Cep { get; set; } = String.Empty;
    public ConsultarCepAsyncRequest(string cep)
    {
        Cep = cep;
    }
}
