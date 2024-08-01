﻿using APIGateway.Domain.CEP.ObjectValues;
using APIGateway.Domain.Interfaces;
using APIGateway.Infrastructure.CEPService.Interfaces;
using Refit;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Net.Http.Json;
using APIGateway.Infrastructure.CEPService.CepException;

namespace APIGateway.Infrastructure.CEPService.Services;



public class CEPService : ICEPRepository
{
    private readonly IRefitCEPService _refitCEPService;

    public CEPService()
    {
        _refitCEPService = RestService.For<IRefitCEPService>("https://viacep.com.br");
    }

    public async Task<CEPObjectValue> ConsultarCepAsync(string cep)
    {
        var client = RestService.For<IRefitCEPService>("https://viacep.com.br", new RefitSettings(){});
        var response = await client.ConsultarCepAsync(cep);

        if (response.IsSuccessStatusCode && response.Content.erro == "false")
        {
            return response.Content;
        }
        else if(!response.IsSuccessStatusCode && response.Content != null)
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