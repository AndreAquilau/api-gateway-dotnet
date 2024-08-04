using APIGateway.Domain.CEP.ObjectValues;
using APIGateway.Domain.CEP.Services;
using APIGateway.Infrastructure.CEPService.Interfaces;
using Refit;

namespace APIGateway.Infrastructure.CEPService.Services;

public class CEPService : ICEPService
{
    private readonly IRefitCEPService _refitCEPService;

    public CEPService()
    {
        _refitCEPService = RestService.For<IRefitCEPService>("https://viacep.com.br");
    }

    public async Task<CEPObjectValue> ConsultarCepAsync(string cep)
    {
        var headers = new Dictionary<string, string> { { "Authorization", "Bearer tokenGoesHere" }, { "X-Tenant-Id", "123" } };
        var rest = RestService.For<IRefitCEPService>("https://viacep.com.br", new RefitSettings() { });
        var response = await rest.ConsultarCepAsync(cep, headers);

        if (response.IsSuccessStatusCode && response.Content.erro == "false")
        {
            return response.Content;
        }
        else if (!response.IsSuccessStatusCode && response.Content != null)
        {
            Console.WriteLine("Erro ao buscar CPF");

            throw new APIGateway.Infrastructure.CEPService.CepException.CepException("Erro ao buscar CPF");
        }
        else
        {
            Console.WriteLine("Erro ao buscar CPF");

            throw new Exception("Erro ao buscar CPF");
        }
    }

    private class TypeError
    {
        public string erro { get; set; } = "true";
    }
}