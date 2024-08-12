using APIGateway.Domain.CEP.ObjectValues;
using APIGateway.Domain.CEP.Services;
using APIGateway.Infrastructure.CEPService.Interfaces;
using Refit;
using System.Text.Json;

namespace APIGateway.Infrastructure.CEPService.Services;

public class CEPService : ICEPService
{
    private readonly IRefitCEPService _refitCEPService;

    public CEPService()
    {
        _refitCEPService = RestService.For<IRefitCEPService>("https://viacep.com.br");
    }

    public async Task<CEPObjectValueOutput> ConsultarCepAsync(string cep)
    {
        var headers = new Dictionary<string, string> { { "Authorization", "Bearer tokenGoesHere" }, { "X-Tenant-Id", "123" } };
        var rest = RestService.For<IRefitCEPService>("https://viacep.com.br", new RefitSettings() { });
        var response = await rest.ConsultarCepAsync(cep, headers);

        if (response.IsSuccessStatusCode && response.Content.erro == "false")
        {
            return response.Content;
        }
        if(response.IsSuccessStatusCode && response.Content.erro == "true")
        {

            Console.WriteLine($"Erro ao buscar CPF: {response?.Content}");

            var error = response?.Content;

            return error ?? new CEPObjectValueOutput() { erro = "true" };
        }
        else
        {
            Console.WriteLine($"Erro ao buscar CPF: {response?.Error?.Content}");

            //throw new APIGateway.Infrastructure.CEPService.CepException.CepException("Erro ao buscar CPF");

            return new CEPObjectValueOutput() { erro = response?.Error?.Message };
        }
    }
}