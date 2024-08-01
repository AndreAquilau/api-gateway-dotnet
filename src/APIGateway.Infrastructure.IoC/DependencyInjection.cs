using Microsoft.Extensions.DependencyInjection;
using APIGateway.Worker;
using APIGateway.Infrastructure.CEPService.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MediatR;
using APIGateway.Application;
using System.Reflection;
using APIGateway.Application.Profiles;

namespace APIGateway.Infrastructure.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IHostedService, APIGateway.Worker.Worker>();
        services.AddScoped<APIGateway.Infrastructure.CEPService.Services.CEPService>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(APIGateway.Application.CEP.UseCases.ConsultarCep.ConsultarCepHandler))));
        services.AddAutoMapper(typeof(DTOMappingProfile));
        return services;
    }
}