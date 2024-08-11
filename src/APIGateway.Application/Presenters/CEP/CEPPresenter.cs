using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIGateway.Application.Presenters.CEP;
public class CEPPresenter
{
    public string cep { get; set; } = String.Empty;
    public string logradouro { get; set; } = String.Empty;
    public string complemento { get; set; } = String.Empty;
    public string unidade { get; set; } = String.Empty;
    public string bairro { get; set; } = String.Empty;
    public string localidade { get; set; } = String.Empty;
    public string uf { get; set; } = String.Empty;
    public string ibge { get; set; } = String.Empty;
    public string gia { get; set; } = String.Empty;
    public string ddd { get; set; } = String.Empty;
    public string siafi { get; set; } = String.Empty;
}
