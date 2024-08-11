using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIGateway.Application.Interfaces;
using APIGateway.Application.Presenters.CEP;
using APIGateway.Domain.CEP.ObjectValues;
using MediatR;

namespace APIGateway.Application.CEP.UseCases.ConsultarCep;
public class ConsultarCepRequest : IRequestBase<CEPPresenter>
{
    public string TransactionId { get; set; }
    public string Cep { get; set; } = String.Empty;
    public ConsultarCepRequest(string cep, string transactionId)
    {
        Cep = cep;
        TransactionId = transactionId;
    }
}
