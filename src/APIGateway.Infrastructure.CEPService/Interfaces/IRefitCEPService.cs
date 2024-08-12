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
    Task<ApiResponse<CEPObjectValueOutput>> ConsultarCepAsync(string cep, [HeaderCollection] IDictionary<string, string> headers);

}