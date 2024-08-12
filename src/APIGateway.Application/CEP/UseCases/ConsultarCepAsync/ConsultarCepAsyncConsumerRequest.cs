using APIGateway.Application.Interfaces;
using APIGateway.Application.Presenters.CEP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIGateway.Application.CEP.UseCases.ConsultarCepAsync;
public class ConsultarCepAsyncConsumerRequest: IRequestBase<CEPPresenter>
{
    public string TransactionId { get; set; }
    public string Cep { get; set; } = String.Empty;
    public ConsultarCepAsyncConsumerRequest(string cep, string transactionId)
    {
        Cep = cep;
        TransactionId = transactionId;
    }
}