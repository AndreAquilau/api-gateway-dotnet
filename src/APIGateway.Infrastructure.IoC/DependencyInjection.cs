using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using APIGateway.Application.Profiles;
using APIGateway.Infrastructure.Kafka;
using Microsoft.Extensions.Configuration;
using Confluent.Kafka;

namespace APIGateway.Infrastructure.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IHostedService, APIGateway.Worker.Worker>();

        services.AddScoped<APIGateway.Infrastructure.CEPService.Services.CEPService>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load($"APIGateway.{nameof(Application)}")));
        services.AddAutoMapper(typeof(DTOMappingProfile));
        return services;
    }


    public static IServiceCollection UseKafka(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ClientKafka>();

        return services;
    }

}