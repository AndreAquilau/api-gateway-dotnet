using Microsoft.Extensions.DependencyInjection;
using APIGateway.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace APIGateway.Infra.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IHostedService, APIGateway.Worker.Worker>();

        return services;
    }
}