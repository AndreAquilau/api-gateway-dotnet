using APIGateway.Domain.CEP.ObjectValues;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace APIGateway.Infrastructure.CEPService.Interfaces;

public interface IRefitCEPService
{
    [Get("/ws/{cep}/json/")]
    Task<ApiResponse<CEPResponse>> ConsultarCepAsync(string cep);

}

public class CEPResponse : CEPObjectValue
{
    public string erro { get; set; } = "false";
}