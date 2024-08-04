using Microsoft.Extensions.DependencyInjection;
using APIGateway.Worker;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using APIGateway.Application.Profiles;
using APIGateway.Infrastructure.Kafka;

namespace APIGateway.Infrastructure.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IHostedService, APIGateway.Worker.Worker>();
        services.AddSingleton<ClientKafka>();
        services.AddScoped<APIGateway.Infrastructure.CEPService.Services.CEPService>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load($"APIGateway.{nameof(Application)}")));
        services.AddAutoMapper(typeof(DTOMappingProfile));
        return services;
    }
}